﻿using Common.Models;


namespace LoginManagement.Interfaces
{
    public interface IAdminAuthenticate
    {
        Tokens Authenticate(UserRegisterTbl admin);
    }
}