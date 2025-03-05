using Cysharp.Threading.Tasks;
using NUnit.Framework;
using SearchTeamFight.CharacterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SearchTeamFight.CharacterSystem.Views.UI
{
    public class HeroesUIView : MonoBehaviour
    {
        [SerializeField] private HeroUIView _heroUIViewPrefab;
        [SerializeField] private LayoutGroup _heroUIViewGroup;
        [SerializeField] private Animator _vsAnimator;

        private readonly List<HeroUIView> _heroUiViews = new();
        private float _clipLenght = 1.0f;
        private const string CLIP_NAME = "VS_anim";

        
        public HeroUIView GetHeroUIView(FighterModel fighterModel)
        {
            var heroUiView = Instantiate(_heroUIViewPrefab, _heroUIViewGroup.transform);
            heroUiView.SetFighterModel(fighterModel);
            _heroUiViews.Add(heroUiView);
            return heroUiView;
        }

        
        public void Show() =>
            gameObject.SetActive(true);

        
        public async UniTask ShowVSAnimation()
        {
            _vsAnimator.gameObject.SetActive(true);

            var clip = _vsAnimator.runtimeAnimatorController.animationClips.First(a => a.name == CLIP_NAME);

            if(clip != null)
                _clipLenght = clip.length;
            else
                Debug.LogError($"Can't find {CLIP_NAME} clip in animator controller, animation time set to default: {_clipLenght} sec.");

            await UniTask.Delay(TimeSpan.FromSeconds(_clipLenght));

            _vsAnimator.gameObject.SetActive(false);
        }

        
        public void Hide() =>
            gameObject.SetActive(false);


        public void Clear()
        {
            foreach (var heroUiView in _heroUiViews)
            {
                Destroy(heroUiView.gameObject);
            }

            _heroUiViews.Clear();
        }
    }
}
