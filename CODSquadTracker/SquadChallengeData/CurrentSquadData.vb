Imports Newtonsoft.Json




Namespace CurrentSquadChal
    Public Class LocalizedName
        <JsonProperty("id")>
        Public Property id As Integer
        <JsonProperty("cmsId")>
        Public Property cmsId As String
        <JsonProperty("language")>
        Public Property language As String
        <JsonProperty("country")>
        Public Property country As String
        <JsonProperty("text")>
        Public Property text As String
    End Class

    Public Class LocalizedDescription
        <JsonProperty("id")>
        Public Property id As Integer
        <JsonProperty("cmsId")>
        Public Property cmsId As String
        <JsonProperty("language")>
        Public Property language As String
        <JsonProperty("country")>
        Public Property country As String
        <JsonProperty("text")>
        Public Property text As String
    End Class

    Public Class FirstPlaceReward
        <JsonProperty("id")>
        Public Property id As Integer
        <JsonProperty("cmsId")>
        Public Property cmsId As String
        <JsonProperty("title")>
        Public Property title As String
        <JsonProperty("type")>
        Public Property type As Integer
        <JsonProperty("localizedDescriptions")>
        Public Property localizedDescriptions As IList(Of LocalizedDescription)
        <JsonProperty("entitlementDetails")>
        Public Property entitlementDetails As String
        <JsonProperty("imageUrl")>
        Public Property imageUrl As String
    End Class

    Public Class SecondPlaceReward
        <JsonProperty("id")>
        Public Property id As Integer
        <JsonProperty("cmsId")>
        Public Property cmsId As String
        <JsonProperty("title")>
        Public Property title As String
        <JsonProperty("type")>
        Public Property type As Integer
        <JsonProperty("localizedDescriptions")>
        Public Property localizedDescriptions As IList(Of LocalizedDescription)
        <JsonProperty("entitlementDetails")>
        Public Property entitlementDetails As String
        <JsonProperty("imageUrl")>
        Public Property imageUrl As String
    End Class

    Public Class ThirdPlaceReward
        <JsonProperty("id")>
        Public Property id As Integer
        <JsonProperty("cmsId")>
        Public Property cmsId As String
        <JsonProperty("title")>
        Public Property title As String
        <JsonProperty("type")>
        Public Property type As Integer
        <JsonProperty("localizedDescriptions")>
        Public Property localizedDescriptions As IList(Of LocalizedDescription)
        <JsonProperty("entitlementDetails")>
        Public Property entitlementDetails As String
        <JsonProperty("imageUrl")>
        Public Property imageUrl As String
    End Class

    Public Class Challenge
        <JsonProperty("id")>
        Public Property id As Integer
        <JsonProperty("cmsId")>
        Public Property cmsId As String
        <JsonProperty("localizedNames")>
        Public Property localizedNames As IList(Of LocalizedName)
        <JsonProperty("mwChallengeType")>
        Public Property mwChallengeType As String
        <JsonProperty("mwChallengeMode")>
        Public Property mwChallengeMode As String
        <JsonProperty("mwChallengeMap")>
        Public Property mwChallengeMap As String
        <JsonProperty("mwProgressCoefficient")>
        Public Property mwProgressCoefficient As Double
        <JsonProperty("mwMinProgress")>
        Public Property mwMinProgress As Double
        <JsonProperty("bo4ChallengeType")>
        Public Property bo4ChallengeType As String
        <JsonProperty("bo4ChallengeMode")>
        Public Property bo4ChallengeMode As String
        <JsonProperty("bo4ChallengeMap")>
        Public Property bo4ChallengeMap As String
        <JsonProperty("bo4ProgressCoefficient")>
        Public Property bo4ProgressCoefficient As Double
        <JsonProperty("bo4MinProgress")>
        Public Property bo4MinProgress As Double
        <JsonProperty("localizedDescriptions")>
        Public Property localizedDescriptions As IList(Of LocalizedDescription)
        <JsonProperty("imageUrl")>
        Public Property imageUrl As Object
        <JsonProperty("startDate")>
        Public Property startDate As DateTime
        <JsonProperty("endDate")>
        Public Property endDate As DateTime
        <JsonProperty("firstPlaceRewards")>
        Public Property firstPlaceRewards As IList(Of FirstPlaceReward)
        <JsonProperty("secondPlaceRewards")>
        Public Property secondPlaceRewards As IList(Of SecondPlaceReward)
        <JsonProperty("thirdPlaceRewards")>
        Public Property thirdPlaceRewards As IList(Of ThirdPlaceReward)
        <JsonProperty("normalizedGoal")>
        Public Property normalizedGoal As Double
    End Class

    Public Class MemberProgress
        <JsonProperty("unoId")>
        Public Property unoId As String
        <JsonProperty("platform")>
        Public Property platform As String
        <JsonProperty("platformId")>
        Public Property platformId As String
        <JsonProperty("gamerTag")>
        Public Property gamerTag As String
        <JsonProperty("avatarUrl")>
        Public Property avatarUrl As String
        <JsonProperty("progress")>
        Public Property progress As Double
    End Class

    Public Class Leaderboard
        <JsonProperty("hash")>
        Public Property hash As String
        <JsonProperty("squadName")>
        Public Property squadName As String
        <JsonProperty("avatarUrl")>
        Public Property avatarUrl As String
        <JsonProperty("progress")>
        Public Property progress As Double
        <JsonProperty("rank")>
        Public Property rank As Integer
    End Class

    Public Class Progress
        <JsonProperty("challengeId")>
        Public Property challengeId As Integer
        <JsonProperty("bo4ChallengeType")>
        Public Property bo4ChallengeType As String
        <JsonProperty("mwChallengeType")>
        Public Property mwChallengeType As Object
        <JsonProperty("minimumProgress")>
        Public Property minimumProgress As Double
        <JsonProperty("overallProgress")>
        Public Property overallProgress As Double
        <JsonProperty("squadName")>
        Public Property squadName As String
        <JsonProperty("memberProgress")>
        Public Property memberProgress As List(Of MemberProgress)
        <JsonProperty("leaderboard")>
        Public Property leaderboard As List(Of Leaderboard)
    End Class

    Public Class Data
        <JsonProperty("phase")>
        Public Property phase As String
        <JsonProperty("challenge")>
        Public Property challenge As Challenge
        <JsonProperty("progress")>
        Public Property progress As Progress
    End Class

    Public Class SquadData
        <JsonProperty("status")>
        Public Property status As String
        <JsonProperty("data")>
        Public Property data As Data
    End Class
End Namespace
