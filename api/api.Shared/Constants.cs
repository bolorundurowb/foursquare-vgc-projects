﻿using System;

namespace api.Shared
{
    public static class Constants
    {
        // NOTE: the MSFT token validator would fail with any value exceeding this
        public static readonly DateTime MaxTokenExpiry = new DateTime(2038, 1, 19, 3, 14, 06);
    }
}