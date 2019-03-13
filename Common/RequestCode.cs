namespace Common
{
    public enum RequestCode
    {
        None,
        Login,
        Register,
        VerifyRepeat,
        SearchUser,
        GetFriendList,
        SetFirstLoginInformation,
        SendAndSaveChatMessage,
        SetReaded,

        //response

        ReciveChatMessage,
        GetNotification,

        SendMessage,
        ShowNotification,


    }
}