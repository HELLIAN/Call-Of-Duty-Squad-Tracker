Imports Newtonsoft.Json

Namespace XboxUser
    Partial Public Class UserData
        <JsonProperty("status")>
        Public Property Status As String
        <JsonProperty("data")>
        Public Property Data As List(Of Datum)
    End Class

    Partial Public Class Datum
        <JsonProperty("platform")>
        Public Property Platform As String
        <JsonProperty("username")>
        Public Property Username As String
        <JsonProperty("avatar")>
        Public Property Avatar As Avatar
    End Class

    Partial Public Class Avatar
        <JsonProperty("avatarUrlLargeSsl")>
        Public Property AvatarUrlLargeSsl As String
        <JsonProperty("avatarUrlMediumSsl")>
        Public Property AvatarUrlMediumSsl As String
        <JsonProperty("avatarUrlSmallSsl")>
        Public Property AvatarUrlSmallSsl As String
    End Class
End Namespace
