﻿using Application.DTOs;
using Application.DTOs.Reponse;
using Application.DTOs.Request;

namespace Application.Interface
{
    public interface IAccountManager
    {
        /// <summary>
        ///     Regrist new user for system
        /// </summary>
        /// <param name="userRegrist">Information off user request make regrist</param>
        /// <returns>
        ///     Return Result Failure If have error while create user
        ///     OtherWise return Success with token
        /// </returns>
        Task<IResult> RegristAsync(RegristViewModel userRegrist);
        /// <summary>
        ///     Login user for system
        /// </summary>
        /// <param name="userLogin">
        ///     Information off user request make login
        /// </param>
        /// <returns>
        ///     Return Result Failure If have error while login user
        ///     Otherwise return success with token
        /// </returns>
        Task<IResult> LoginAsync(LoginViewModel userLogin);
        /// <summary>
        ///     Logout user for system
        /// </summary>
        /// <param name="userId">
        ///     User recall refresh token with assign refresh token null 
        /// </param>
        /// <returns>
        ///     Return Failure if have error while logout
        ///     otherwise return success logout
        /// </returns>
        Task<IResult> LogoutAsync(string userId);
        /// <summary>
        ///     Get new token when token has exprise
        /// </summary>
        /// <param name="tokenClaim">
        ///     Old token
        /// </param>
        /// <returns>
        ///     Return Failure if have error while create new token
        ///     otherwise return success with new token
        /// </returns>
        Task<IResult> GetTokenAsync(TokenClaimsDTO tokenClaim);
        /// <summary>
        ///     Send email for user confirm
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     Return result success if email send success for user
        ///     otherwise is result is fail
        /// </returns>
        Task<IResult> SendConfirmEmailTokenAsync(string userId);
        /// <summary>
        ///     Check email token could correct
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns>
        ///     Return Result Success if token is match of user and token has not exprise
        ///     otherwise Result Fail
        /// </returns>
        Task<IResult> ConfirmEmailTokenAsync(string userId, string token);
        /// <summary>
        ///     Change password for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userChangePassword"></param>
        /// <returns>
        ///     Return success if change password is success
        ///     otherwise Fail
        /// </returns>
        Task<IResult> ChangePasswordAsync(string userId, UserChangePasswordViewModel userChangePassword);
        /// <summary>
        ///     Forgot password
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IResult> SendEmailForgotPasswordAsync(string email);
        /// <summary>
        ///  Reset your password
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IResult> ResetPasswordAsync(string email, string token);
        /// <summary>
        ///     Update user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IResult> UpdateProfileForUserAsync(string userId, UpdateUserDetailViewModel profile);
        /// <summary>
        /// User for get detail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     Return user 
        /// </returns>
        Task<UserDetailReponse?> GetInformationForUserAsync(string userId);
    }
}
