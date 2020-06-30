Imports Newtonsoft.Json

Namespace SteamUser
    Public Class Avatar
        <JsonProperty("avatarUrlLargeSsl")>
        Public Property AvatarUrlLargeSsl As String
        <JsonProperty("avatarUrlMediumSsl")>
        Public Property AvatarUrlMediumSsl As String
        <JsonProperty("avatarUrlSmallSsl")>
        Public Property AvatarUrlSmallSsl As String
    End Class

    Public Class Datum
        <JsonProperty("platform")>
        Public Property Platform As String
        <JsonProperty("username")>
        Public Property Username As String
        <JsonProperty("avatar")>
        Public Property Avatar As Avatar
    End Class

    Public Class Userdata
        <JsonProperty("status")>
        Public Property Status As String
        <JsonProperty("data")>
        Public Property Data As List(Of Datum)
    End Class
End Namespace
