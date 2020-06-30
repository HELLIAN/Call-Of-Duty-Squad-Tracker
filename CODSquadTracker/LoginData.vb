Imports Newtonsoft.Json
Imports RestSharp

Public Class LoginData
    Public Sub GetSSO(Uname As String, Upass As String)
        Dim DevID As String = System.Guid.NewGuid.ToString.Replace("-", "")
        Dim DevAuth As String

        Try
            Dim client = New RestClient("https://profile.callofduty.com/cod/mapp/registerDevice")
            Dim postrequest = New RestRequest(Method.[POST])
            postrequest.AddHeader("Connection", "keep-alive")
            postrequest.AddHeader("Accept-Encoding", "gzip, deflate")
            postrequest.AddHeader("Accept", "*/*")
            postrequest.AddParameter("application/json", "{""deviceId"": """ & DevID & """ }", ParameterType.RequestBody)
            client.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            Dim response As IRestResponse = client.Execute(postrequest)
            Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response.Content)
            DevAuth = jsonResulttodict.Item("data")("authHeader")
        Catch ex As Exception
            MsgBox("There was an error logging in Device Registration.")
        End Try

        Try
            Dim client = New RestClient("https://profile.callofduty.com/cod/mapp/login")
            Dim postrequest = New RestRequest(Method.[POST])
            postrequest.AddHeader("cache-control", "no-cache")
            postrequest.AddHeader("Connection", "keep-alive")
            postrequest.AddHeader("Accept-Encoding", "gzip, deflate")
            postrequest.AddHeader("Accept", "*/*")
            postrequest.AddHeader("Authorization", "bearer " & "" & DevAuth & "")
            postrequest.AddHeader("x_cod_device_id", "" & DevID & "")
            postrequest.AddParameter("application/json", "{ ""email"": """ & Uname & """, ""password"": """ & Upass & """ }", ParameterType.RequestBody)
            client.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            Dim response As IRestResponse = client.Execute(postrequest)
            Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response.Content)
            Dim Sec As New Security
            My.Settings.Userkey = Sec.Encrypt(jsonResulttodict.Item("ACT_SSO_COOKIE"))
        Catch ex As Exception
            My.Settings.Userkey = ""
            MsgBox("There was an error logging in, unable to get user key.")
        End Try
    End Sub
End Class
