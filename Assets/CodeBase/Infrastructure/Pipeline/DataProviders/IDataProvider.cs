using System;

namespace Infrastructure.Pipeline.DataProviders
{
    public interface IDataProvider
    {
        public Type ModelType { get; }
    }
}