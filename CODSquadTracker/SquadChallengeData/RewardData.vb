Imports Newtonsoft.Json

Namespace CheckReward
    Public Class LocalizedDescription
        <JsonProperty("id")>
        Public Property Id As Integer
        <JsonProperty("cmsId")>
        Public Property CmsId As String
        <JsonProperty("language")>
        Public Property Language As String
        <JsonProperty("country")>
        Public Property Country As String
        <JsonProperty("text")>
        Public Property Text As String
    End Class

    Public Class Datum
        <JsonProperty("id")>
        Public Property Id As Integer
        <JsonProperty("cmsId")>
        Public Property CmsId As String
        <JsonProperty("title")>
        Public Property Title As String
        <JsonProperty("type")>
        Public Property Type As Integer
        <JsonProperty("localizedDescriptions")>
        Public Property LocalizedDescriptions As IList(Of LocalizedDescription)
        <JsonProperty("entitlementDetails")>
        Public Property EntitlementDetails As String
        <JsonProperty("imageUrl")>
        Public Property ImageUrl As String
    End Class

    Public Class Reward
        <JsonProperty("status")>
        Public Property Status As String
        <JsonProperty("data")>
        Public Property Data As IList(Of Datum)
    End Class

End Namespace