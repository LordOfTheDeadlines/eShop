﻿using System.Collections.Generic;

namespace AdminWebApp.ViewModels
{
    public class UserInfoViewModel
    {
        public Dictionary<string, string> UserInfoDictionary { get; private set; } = null;

        public UserInfoViewModel(Dictionary<string, string> userInfoDictionary)
        {
            UserInfoDictionary = userInfoDictionary;
        }
    }
}
