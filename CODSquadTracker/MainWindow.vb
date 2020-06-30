
' ****************************************************************************************************
' CALL OF DUTY SQUAD TRACKER
'
' CREATED BY ABSTRACTTORO@GMAIL.COM / HELLIAN@KNIGHTSOFSIX.COM
'
' NO AFFILIATION WITH ACTIVISION, INFINITY WARD, SLEDGHAMMER GAMES, TREYARCH
' APPLICATION IS FOR GETTING SQUAD DATA FROM CALL OF DUTY API AND DISPLAYING DATA TO USER.
'*****************************************************************************************************

Imports MahApps.Metro.Controls.Dialogs
Imports Newtonsoft.Json
Imports RestSharp
Imports System.Windows.Threading

Class MainWindow

    Dim Counter As Integer = 90
    Dim Timer1 As New DispatcherTimer()
    Dim Timer2 As New DispatcherTimer()
    Dim Platform As String
    Dim endate As Date
    Dim enc As New Security

    Private Sub Onload(sender As Object, e As RoutedEventArgs)
        GridLeaderboards.Visibility = Visibility.Visible
        GridFoundUsers.Visibility = Visibility.Collapsed
        GridSquadList.Visibility = Visibility.Collapsed
        ObjectiveBar.Visibility = Visibility.Visible
        MySquadBar.Visibility = Visibility.Collapsed
        RewardsFly.IsOpen = False
        Settings.IsOpen = False
        Timer1.Interval = TimeSpan.FromMilliseconds(1000)
        AddHandler Timer1.Tick, AddressOf Timer1_Tick
        Timer2.Interval = TimeSpan.FromMilliseconds(1000)
        AddHandler Timer2.Tick, AddressOf Timer2_Tick
        If My.Settings.SaveCheck = False Then
            My.Settings.Reset()
            OnStat.Content = "NOT LOGGED IN"
            Settings.IsOpen = True
        Else
            If My.Settings.Userkey.Count > 0 Then
                SaveCredsBox.IsChecked = My.Settings.SaveCheck
                Emailtxt.Text = enc.Decrypt(My.Settings.Username)
                Passtxt.Password = enc.Decrypt(My.Settings.Password)
                OnStat.Content = "LOGGED IN"
                GetCurrentChallengeData()
                GetMySquadData()
                Timer1.Start()
                Timer2.Start()
            End If
        End If
    End Sub

    Private Sub TabLeaderboard_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        GridLeaderboards.Visibility = Visibility.Visible
        GridFoundUsers.Visibility = Visibility.Collapsed
        GridSquadList.Visibility = Visibility.Collapsed
        ObjectiveBar.Visibility = Visibility.Visible
        MySquadBar.Visibility = Visibility.Collapsed
        RewardsFly.IsOpen = False
        Settings.IsOpen = False
    End Sub

    Private Sub TabSquadList_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        GridLeaderboards.Visibility = Visibility.Collapsed
        GridFoundUsers.Visibility = Visibility.Collapsed
        GridSquadList.Visibility = Visibility.Visible
        ObjectiveBar.Visibility = Visibility.Collapsed
        MySquadBar.Visibility = Visibility.Visible
        RewardsFly.IsOpen = False
        Settings.IsOpen = False
    End Sub

    Private Sub textBox_Selected(sender As Object, e As RoutedEventArgs)
        GridLeaderboards.Visibility = Visibility.Collapsed
        GridFoundUsers.Visibility = Visibility.Visible
        GridSquadList.Visibility = Visibility.Collapsed
        ObjectiveBar.Visibility = Visibility.Collapsed
        MySquadBar.Visibility = Visibility.Collapsed
        RewardsFly.IsOpen = False
        Settings.IsOpen = False
    End Sub

    Private Sub Rewards_MouseDown(sender As Object, e As MouseButtonEventArgs)
        RewardsFly.IsOpen = True
        Settings.IsOpen = False
    End Sub

    Private Sub Settings_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Settings.IsOpen = True
        RewardsFly.IsOpen = False
    End Sub

    Private Sub listBoxPlatform_DropDownClosed(sender As Object, e As EventArgs)
        Select Case listBoxPlatform.SelectionBoxItem.ToString()
            Case "XBOX"
                Platform = "xbl"
            Case "PLAYSTATION"
                Platform = "psn"
            Case "STEAM"
                Platform = "steam"
            Case "BATTLE.NET"
                Platform = "battle"
            Case "ACTIVISION"
                Platform = "uno"
            Case Else
                MsgBox("Unknown type")
        End Select
    End Sub

    Private Sub textBox_KeyDown(sender As Object, e As KeyEventArgs)
        If Platform = "" Then
            Platform = "uno"
        End If
        If e.Key = Key.Enter Then
            SearchUser(Platform, UserSeachBox.Text)
        End If
    End Sub

    Private Sub Timer1_Tick()
        Counter -= 1
        CountTxt.Content = Counter
        CountTxt.UpdateLayout()

        If Counter = 0 Then
            Timer1.Stop()
            CountTxt.Content = "Refreshing..."
            Threading.Thread.Sleep(1000)
            GetCurrentChallengeData()
            GetMySquadData()
            Counter = 90
            Timer1.Start()
        End If
    End Sub

    Private Sub Timer2_Tick()
        Dim startTime As DateTime = DateTime.Now
        Dim endTime As DateTime = DateTime.Parse(endate.ToLocalTime)
        Dim span As TimeSpan = endTime.Subtract(startTime)
        RemainTxt.Content = span.Days.ToString & "d " & span.Hours.ToString & "h " & span.Minutes.ToString & "m " & span.Seconds.ToString & "s"
        RemainTxt.UpdateLayout()
    End Sub



    Private Sub buttonLogin_Click(sender As Object, e As RoutedEventArgs)
        Dim ld As New LoginData
        ld.GetSSO(Emailtxt.Text, Passtxt.Password)
        If My.Settings.Userkey.Count > 0 Then
            If SaveCredsBox.IsChecked Then
                My.Settings.SaveCheck = True

                My.Settings.Username = enc.Encrypt(Emailtxt.Text)
                My.Settings.Password = enc.Encrypt(Passtxt.Password)
                My.Settings.Save()
            Else
                My.Settings.SaveCheck = False
                My.Settings.Username = ""
                My.Settings.Password = ""
            End If
            Settings.IsOpen = False
            ShowMessageAsync("Login Status", "Login Successful!", MessageDialogStyle.Affirmative)
            OnStat.Content = "LOGGED IN"
            GetCurrentChallengeData()
            GetMySquadData()
            Timer1.Start()
            Timer2.Start()
        Else
            My.Settings.Reset()
            Settings.IsOpen = False
            ShowMessageAsync("Login Status", "Login Unsuccessful!", MessageDialogStyle.Affirmative)
            OnStat.Content = "ERROR LOGIN"
            Emailtxt.Text = ""
            Passtxt.Password = ""
        End If
    End Sub


    Private Sub buttonReset_Click(sender As Object, e As RoutedEventArgs)
        Timer1.Stop()
        Timer2.Stop()
        My.Settings.Reset()
        Emailtxt.Clear()
        Passtxt.Clear()
        SaveCredsBox.IsChecked = False
        My.Settings.SaveCheck = False
        Settings.IsOpen = False
    End Sub

    Private Sub buttonLogout_Click(sender As Object, e As RoutedEventArgs)
        Timer1.Stop()
        Timer2.Stop()
        My.Settings.Reset()
        OnStat.Content = "NOT LOGGED IN"
        Settings.IsOpen = True
        DataGridLeaderboard.ItemsSource = ""
        DataGridFoundUser.ItemsSource = ""
        DataGridSquadList.ItemsSource = ""
        SquadNameTxt.Content = ""
        SqaudGenTxt.Content = ""
        SqaudAboutTxt.Content = ""
        SquadMemberCountTxt.Content = ""
        RemainTxt.Content = ""
        CurrentPos.Content = ""

    End Sub


    Public Sub GetCurrentChallengeData()

        'Need to add feature in to handle if there are no active weekly Tournaments.
        'Notice on mobile app says under weekly tournament "No Active Tournamnet. Weekly Tournament is being set up. Check Back Shortly."
        'Check saved data for image or text file in project folder on share drive.

        Try
            Dim client = New RestClient("https://squads.callofduty.com/api/v2/challenge/lookup/current")
            Dim request = New RestRequest(Method.[GET])
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("Connection", "keep-alive")
            request.AddHeader("Accept-Encoding", "gzip, deflate")
            request.AddHeader("Host", "squads.callofduty.com")
            request.AddHeader("Accept", "*/*")
            client.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            request.AddParameter("ACT_SSO_COOKIE", enc.Decrypt(My.Settings.Userkey), ParameterType.Cookie)
            Dim response As IRestResponse = client.Execute(request)
            Dim SquadData As CurrentSquadChal.SquadData = JsonConvert.DeserializeObject(Of CurrentSquadChal.SquadData)(response.Content.ToString)

            If SquadData.status.ToString = "success" Then
                OnStat.UpdateLayout()
            Else
                OnStat.Content = "ERROR"
                ShowMessageAsync("ERROR", "Unable to get Challenge Data", MessageDialogStyle.Affirmative)
            End If
            Type.Content = SquadData.data.challenge.localizedNames.Item(0).text.ToUpper.ToString
            Description.Content = SquadData.data.challenge.localizedDescriptions.Item(0).text.ToUpper.ToString
            Goal.Content = SquadData.data.challenge.mwChallengeType.ToUpper.ToString
            endate = SquadData.data.challenge.endDate.ToString

            FP_RW1.Content = SquadData.data.challenge.firstPlaceRewards.Item(0).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.firstPlaceRewards.Item(0).title.ToUpper.ToString & ")"
            FP_RW2.Content = SquadData.data.challenge.firstPlaceRewards.Item(1).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.firstPlaceRewards.Item(1).title.ToUpper.ToString & ")"
            FP_RW3.Content = SquadData.data.challenge.firstPlaceRewards.Item(2).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.firstPlaceRewards.Item(2).title.ToUpper.ToString & ")"
            SP_RW1.Content = SquadData.data.challenge.secondPlaceRewards.Item(0).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.secondPlaceRewards.Item(0).title.ToUpper.ToString & ")"
            SP_RW2.Content = SquadData.data.challenge.secondPlaceRewards.Item(1).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.secondPlaceRewards.Item(1).title.ToUpper.ToString & ")"
            TP_RW1.Content = SquadData.data.challenge.thirdPlaceRewards.Item(0).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.thirdPlaceRewards.Item(0).title.ToUpper.ToString & ")"
            TP_RW2.Content = SquadData.data.challenge.thirdPlaceRewards.Item(1).localizedDescriptions.Item(0).text.ToString & " " & "(" & SquadData.data.challenge.thirdPlaceRewards.Item(1).title.ToUpper.ToString & ")"


            DataGridLeaderboard.ItemsSource = SquadData.data.progress.leaderboard
            DataGridSquadList.ItemsSource = SquadData.data.progress.memberProgress


            Dim Squadcount As Integer = SquadData.data.progress.leaderboard.Count
            Dim ii As Integer
            For ii = 0 To Squadcount - 1
                If SquadData.data.progress.leaderboard.Item(ii).squadName.ToString = SquadData.data.progress.squadName.ToString Then
                    CurrentPos.Content = SquadData.data.progress.leaderboard.Item(ii).rank.ToString
                End If
            Next ii
        Catch ex As Exception
            If ex.ToString.Contains("Unauthorized") Then
                ShowMessageAsync("Error", "Unauthorized, you need to sign in!", MessageDialogStyle.Affirmative)
            End If
        End Try
    End Sub


    Public Sub GetMySquadData()

        'Need to setup process for Squad / Leader to be able to remove users from squad.
        'Also setup proccess for Leader to be able to invite from searched users.

        Try
            Dim client = New RestClient("https://squads.callofduty.com/api/v2/squad/lookup/mine/")
            Dim request = New RestRequest(Method.[GET])
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("Connection", "keep-alive")
            request.AddHeader("Accept-Encoding", "gzip, deflate")
            request.AddHeader("Host", "squads.callofduty.com")
            request.AddHeader("Accept", "*/*")
            client.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            request.AddParameter("ACT_SSO_COOKIE", enc.Decrypt(My.Settings.Userkey), ParameterType.Cookie)
            Dim response As IRestResponse = client.Execute(request)
            Dim MySquadData As MySquad.CurrentSquad = JsonConvert.DeserializeObject(Of MySquad.CurrentSquad)(response.Content.ToString)

            If MySquadData.Status.ToString = "success" Then
                OnStat.UpdateLayout()
            Else
                OnStat.Content = "ERROR"
                ShowMessageAsync("ERROR", "Unable to get Squad Data", MessageDialogStyle.Affirmative)
            End If

            SquadNameTxt.Content = MySquadData.Data.Name.ToString
            SqaudGenTxt.Content = MySquadData.Data.Creator.GamerTag.ToString
            SqaudAboutTxt.Content = MySquadData.Data.Description.ToString
            SquadMemberCountTxt.Content = MySquadData.Data.Members.Count.ToString & "/20"



        Catch ex As Exception
            If ex.ToString.Contains("Unauthorized") Then
                ShowMessageAsync("Error", "Unauthorized, you need to sign in!", MessageDialogStyle.Affirmative)
            End If
        End Try
    End Sub


    '*************************************************************************************************
    ' THIS SECTION IS WORK IN PROGRESS. ITS NOT SETUP YET.
    '*************************************************************************************************

    Public Sub CheckRewards()
        Try
            Dim client = New RestClient("https://squads.callofduty.com/api/v2/rewards/lookup/current")
            Dim request = New RestRequest(Method.[GET])
            request.AddHeader("Connection", "keep-alive")
            request.AddHeader("Accept-Encoding", "gzip, deflate")
            request.AddHeader("Host", "squads.callofduty.com")
            request.AddHeader("Cache-Control", "no-cache")
            request.AddHeader("Accept", "*/*")
            client.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            request.AddParameter("ACT_SSO_COOKIE", enc.Decrypt(My.Settings.Userkey), ParameterType.Cookie)
            Dim response As IRestResponse = client.Execute(request)
            Dim RewardData As CheckReward.Reward = JsonConvert.DeserializeObject(Of CheckReward.Reward)(response.Content.ToString)

            If RewardData.Status.ToString = "success" Then

                'CHECK REWARD DATA HAS REWARDS FOR YOU TO REDEEM, IF IT DOES THEN THROW FLYOUT

            End If
        Catch ex As Exception
            If ex.ToString.Contains("Unauthorized") Then
                ShowMessageAsync("Error", "Unauthorized, you need to sign in!", MessageDialogStyle.Affirmative)
            End If
        End Try
    End Sub

    '*************************************************************************************************
    '*************************************************************************************************


    Public Sub SearchUser(platform As String, user As String)
        Try
            Dim client = New RestClient("https://my.callofduty.com/api/papi-client/crm/cod/v2/platform/" & platform & "/username/" & user & "/search")
            Dim request = New RestRequest(Method.[GET])
            request.AddHeader("cache-control", "no-cache")
            request.AddHeader("Connection", "keep-alive")
            request.AddHeader("Accept-Encoding", "gzip, deflate")
            request.AddHeader("Accept", "*/*")
            client.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            request.AddParameter("ACT_SSO_COOKIE", enc.Decrypt(My.Settings.Userkey), ParameterType.Cookie)
            Dim response As IRestResponse = client.Execute(request)

            If platform = "uno" Then
                Dim UnoSearchData As ActivisionUser.Userdata = JsonConvert.DeserializeObject(Of ActivisionUser.Userdata)(response.Content.ToString)
                If UnoSearchData.Status.ToString = "success" Then
                    OnStat.UpdateLayout()
                    DataGridFoundUser.ItemsSource = UnoSearchData.Data
                Else
                    OnStat.Content = "ERROR ACTAPI"
                End If
            ElseIf platform = "psn" Then
                Dim PsnSearchData As PlaystationUser.Userdata = JsonConvert.DeserializeObject(Of PlaystationUser.Userdata)(response.Content.ToString)
                If PsnSearchData.Status.ToString = "success" Then
                    OnStat.UpdateLayout()
                    DataGridFoundUser.ItemsSource = PsnSearchData.Data
                Else
                    OnStat.Content = "ERROR PSNAPI"
                End If
            ElseIf platform = "xbl" Then
                Dim XblSearchData As XboxUser.UserData = JsonConvert.DeserializeObject(Of XboxUser.UserData)(response.Content.ToString)
                If XblSearchData.Status.ToString = "success" Then
                    OnStat.UpdateLayout()
                    DataGridFoundUser.ItemsSource = XblSearchData.Data
                Else
                    OnStat.Content = "ERROR XBLAPI"
                End If
            ElseIf platform = "battle" Then
                Dim BattleSearchData As BattlenetUser.Userdata = JsonConvert.DeserializeObject(Of BattlenetUser.Userdata)(response.Content.ToString)
                If BattleSearchData.Status.ToString = "success" Then
                    OnStat.UpdateLayout()
                    DataGridFoundUser.ItemsSource = BattleSearchData.Data
                Else
                    OnStat.Content = "ERROR BATTLEAPI"
                End If
            ElseIf platform = "steam" Then
                Dim SteamSearchData As SteamUser.Userdata = JsonConvert.DeserializeObject(Of SteamUser.Userdata)(response.Content.ToString)
                If SteamSearchData.Status.ToString = "success" Then
                    OnStat.UpdateLayout()
                    DataGridFoundUser.ItemsSource = SteamSearchData.Data
                Else
                    OnStat.Content = "ERROR BATTLEAPI"
                End If
            Else
                MsgBox("Unknown platform")
                Return
            End If

        Catch ex As Exception
            If ex.ToString.Contains("Unauthorized") Then
                ShowMessageAsync("Error", "Unauthorized, you need to sign in!", MessageDialogStyle.Affirmative)
            End If
        End Try
    End Sub


    Private Sub ReportSquad_Click(sender As Object, e As RoutedEventArgs)
        Dim item As Object = DataGridLeaderboard.SelectedItem
        Dim Sname As String = (TryCast(DataGridLeaderboard.SelectedCells(1).Column.GetCellContent(item), TextBlock)).Text
        Try
            Dim clientl = New RestClient("https://squads.callofduty.com/api/v2/squad/lookup/name/" & Sname)
            Dim requestg = New RestRequest(Method.[GET])
            requestg.AddHeader("cache-control", "no-cache")
            requestg.AddHeader("Connection", "keep-alive")
            requestg.AddHeader("Accept-Encoding", "gzip, deflate")
            requestg.AddHeader("Host", "squads.callofduty.com")
            requestg.AddHeader("Accept", "*/*")
            clientl.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
            requestg.AddParameter("ACT_SSO_COOKIE", enc.Decrypt(My.Settings.Userkey), ParameterType.Cookie)
            Dim responsel As IRestResponse = clientl.Execute(requestg)
            Dim LookupSquadData As SquadLookup.SquadData = JsonConvert.DeserializeObject(Of SquadLookup.SquadData)(responsel.Content.ToString)
            If LookupSquadData.Status.ToString = "success" Then
                Dim result = MessageBox.Show("Are you sure you want to report this squad?", "Are you sure?", MessageBoxButton.YesNo)
                If result = result.Yes Then
                    Try
                        Dim clientr = New RestClient("https://squads.callofduty.com/api/v2/squad/report/" & LookupSquadData.Data.Hash)
                        Dim requestp = New RestRequest(Method.[GET])
                        requestp.AddHeader("cache-control", "no-cache")
                        requestp.AddHeader("Connection", "keep-alive")
                        requestp.AddHeader("Accept-Encoding", "gzip, deflate")
                        requestp.AddHeader("Host", "squads.callofduty.com")
                        requestp.AddHeader("Accept", "*/*")
                        clientr.UserAgent = "CST/1.0 (SQUAD TRACKER 1.0)"
                        requestp.AddParameter("ACT_SSO_COOKIE", enc.Decrypt(My.Settings.Userkey), ParameterType.Cookie)
                        Dim responsep As IRestResponse = clientr.Execute(requestp)
                        Dim SquadReport As SquadLookup.SquadData = JsonConvert.DeserializeObject(Of SquadLookup.SquadData)(responsel.Content.ToString)
                        If SquadReport.Status.ToString = "success" Then
                            MsgBox("Squad " & LookupSquadData.Data.Name & " reported.")
                        Else
                            ShowMessageAsync("ERROR", "Unable to report squad.", MessageDialogStyle.Affirmative)
                        End If
                    Catch ex As Exception
                        If ex.ToString.Contains("Unauthorized") Then
                            ShowMessageAsync("Error", "Unauthorized, you need to sign in!", MessageDialogStyle.Affirmative)
                        End If
                    End Try
                ElseIf result = result.No Then
                    MsgBox("Report Aborted.")
                End If
            Else
                ShowMessageAsync("ERROR", "Unable to get Squad Data", MessageDialogStyle.Affirmative)
            End If
        Catch ex As Exception
            If ex.ToString.Contains("Unauthorized") Then
                ShowMessageAsync("Error", "Unauthorized, you need to sign in!", MessageDialogStyle.Affirmative)
            End If
        End Try
    End Sub
End Class
