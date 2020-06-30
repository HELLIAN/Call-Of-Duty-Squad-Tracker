Imports Newtonsoft.Json

Namespace ActivisionUser
    Public Class Datum
        <JsonProperty("platform")>
        Public Property Platform As String
        <JsonProperty("username")>
        Public Property Username As String
        <JsonProperty("accountId")>
        Public Property AccountId As String
        <JsonProperty("avatar")>
        Public Property Avatar As Object
    End Class

    Public Class Userdata
        <JsonProperty("status")>
        Public Property Status As String
        <JsonProperty("data")>
        Public Property Data As List(Of Datum)
    End Class
End Namespace