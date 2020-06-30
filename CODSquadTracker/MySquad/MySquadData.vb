Imports Newtonsoft.Json

Namespace MySquad
    Public Class Creator
        <JsonProperty("unoId")>
        Public Property UnoId As String
        <JsonProperty("platform")>
        Public Property Platform As String
        <JsonProperty("platformId")>
        Public Property PlatformId As String
        <JsonProperty("gamerTag")>
        Public Property GamerTag As String
        <JsonProperty("officer")>
        Public Property Officer As Object
        <JsonProperty("avatarUrl")>
        Public Property AvatarUrl As String
    End Class

    Public Class Member
        <JsonProperty("unoId")>
        Public Property UnoId As String
        <JsonProperty("platform")>
        Public Property Platform As String
        <JsonProperty("platformId")>
        Public Property PlatformId As String
        <JsonProperty("gamerTag")>
        Public Property GamerTag As String
        <JsonProperty("officer")>
        Public Property Officer As Boolean?
        <JsonProperty("avatarUrl")>
        Public Property AvatarUrl As String
    End Class

    Public Class Data
        <JsonProperty("id")>
        Public Property Id As Object
        <JsonProperty("creator")>
        Public Property Creator As Creator
        <JsonProperty("createDate")>
        Public Property CreateDate As DateTime
        <JsonProperty("name")>
        Public Property Name As String
        <JsonProperty("description")>
        Public Property Description As String
        <JsonProperty("avatarUrl")>
        Public Property AvatarUrl As String
        <JsonProperty("href")>
        Public Property Href As Object
        <JsonProperty("closed")>
        Public Property Closed As Boolean
        <JsonProperty("points")>
        Public Property Points As Integer
        <JsonProperty("members")>
        Public Property Members As IList(Of Member)
        <JsonProperty("hash")>
        Public Property Hash As String
        <JsonProperty("newlyFormed")>
        Public Property NewlyFormed As Boolean
    End Class

    Public Class CurrentSquad
        <JsonProperty("status")>
        Public Property Status As String
        <JsonProperty("data")>
        Public Property Data As Data
    End Class

End Namespace