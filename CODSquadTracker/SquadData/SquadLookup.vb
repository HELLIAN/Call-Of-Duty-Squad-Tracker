Imports Newtonsoft.Json

Namespace SquadLookup
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
        Public Property Officer As Boolean

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
        Public Property Officer As Boolean

        <JsonProperty("avatarUrl")>
        Public Property AvatarUrl As String
    End Class

    Public Class MemberProgress

        <JsonProperty("unoId")>
        Public Property UnoId As String

        <JsonProperty("platform")>
        Public Property Platform As String

        <JsonProperty("platformId")>
        Public Property PlatformId As String

        <JsonProperty("gamerTag")>
        Public Property GamerTag As String

        <JsonProperty("avatarUrl")>
        Public Property AvatarUrl As String

        <JsonProperty("progress")>
        Public Property Progress As Double
    End Class

    Public Class Leaderboard

        <JsonProperty("hash")>
        Public Property Hash As String

        <JsonProperty("squadName")>
        Public Property SquadName As String

        <JsonProperty("avatarUrl")>
        Public Property AvatarUrl As String

        <JsonProperty("progress")>
        Public Property Progress As Double

        <JsonProperty("rank")>
        Public Property Rank As Integer
    End Class

    Public Class Progress

        <JsonProperty("challengeId")>
        Public Property ChallengeId As Integer

        <JsonProperty("bo4ChallengeType")>
        Public Property Bo4ChallengeType As String

        <JsonProperty("mwChallengeType")>
        Public Property MwChallengeType As Object

        <JsonProperty("minimumProgress")>
        Public Property MinimumProgress As Double

        <JsonProperty("overallProgress")>
        Public Property OverallProgress As Double

        <JsonProperty("squadName")>
        Public Property SquadName As String

        <JsonProperty("memberProgress")>
        Public Property MemberProgress As MemberProgress()

        <JsonProperty("leaderboard")>
        Public Property Leaderboard As Leaderboard()
    End Class

    Public Class Data

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

        <JsonProperty("closed")>
        Public Property Closed As Boolean

        <JsonProperty("points")>
        Public Property Points As Integer

        <JsonProperty("members")>
        Public Property Members As Member()

        <JsonProperty("hash")>
        Public Property Hash As String

        <JsonProperty("newlyFormed")>
        Public Property NewlyFormed As Boolean

        <JsonProperty("progress")>
        Public Property Progress As Progress
    End Class

    Public Class SquadData

        <JsonProperty("status")>
        Public Property Status As String

        <JsonProperty("data")>
        Public Property Data As Data
    End Class


End Namespace