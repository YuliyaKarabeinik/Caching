﻿using System;

namespace Infrastructure
{
    public interface ICache
    {
        T Get<T>(string key);

        void Set(string key, object value, DateTime expirationTime);
    }
}