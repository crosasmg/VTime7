'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.36.1 at 2020-03-30 05:41:33 p. m. model release 1, Form Generator v1.0.37.37
'     
'     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
' </generated>
'---------------------------------------------------------------------------------------------------
      
#Region "using"

Imports System.Data
Imports System.Globalization
Imports System.Net
Imports System.Runtime.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.FrontOffice.Support

#End Region

Namespace dli.forms

    Public Class CrystalReportServiceSampleActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"

       <WebMethod(EnableSession:=True)>
        Public Shared Function Initialization(id As String, urlid As String, fromid As String) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim instance As New EntryData With {.InstanceFormId = id}
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim currentAction As String = String.Empty
            
            Dim CrystalReportServiceSampleParametersInstance As CrystalReportServiceSampleParameters = Nothing

            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                
                If id.IsEmpty AndAlso urlid.IsEmpty Then
                    instance.InstanceFormId = System.Guid.NewGuid().ToString
                    
                    With instance
                        .ClientClientID = "00000013126341"
                    End With                
                    
                    CrystalReportServiceSampleParametersInstance = EntryDataToClass(CrystalReportServiceSampleParametersInstance, instance)
                    SetDefaultValuesFromQueryString(CrystalReportServiceSampleParametersInstance)
                    
                    If fromid.IsNotEmpty Then
                        GetTransferParameters(CrystalReportServiceSampleParametersInstance, fromid)
                        ValidateParametersInstance(CrystalReportServiceSampleParametersInstance)
                    End If
                Else       
                    instance.InstanceFormId = id.IfEmpty(urlid)
                    CrystalReportServiceSampleParametersInstance = RetrieveFormInformationFromSession(instance.InstanceFormId)
         
                    If IsNothing(CrystalReportServiceSampleParametersInstance) then
                        CrystalReportServiceSampleParametersInstance = EntryDataToClass(CrystalReportServiceSampleParametersInstance, instance)                        
                    End If
                    
                    SetDefaultValuesFromQueryString(CrystalReportServiceSampleParametersInstance)
                End If

                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension("Page.AppRelativeVirtualPath"))) = String.Empty



                instance = ClassToEntryData(instance, CrystalReportServiceSampleParametersInstance) 
		        StoreFormInformationOnSession(CrystalReportServiceSampleParametersInstance)
                
                                
                With resultData
                    .Success = True
                    .Data = New With {.Instance = instance, .LookUps = Nothing}
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "CrystalReportServiceSample", "Initialization", currentAction)
            End Try
            
            Return resultData
        End Function
  

        <WebMethod(EnableSession:=True)>
        Public Shared Function button2Click(instance As EntryData) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim CrystalReportServiceSampleParametersInstance As CrystalReportServiceSampleParameters = Nothing
            Dim UserInfo As InMotionGIT.Membership.Providers.MemberContext = Nothing
            Dim formContext As InMotionGIT.Common.Contracts.Context = Nothing
            Dim selectDataTableItem As DataTable = Nothing
            Dim currentAction As String = String.Empty
            Dim messageAction As String = String.Empty
            Dim parametersDictionary As Dictionary(Of String, Object) = Nothing
            Dim WorkflowInArguments As Dictionary(Of String, Object) = Nothing
            Dim WorkflowOutArguments As IDictionary(Of String, Object) = Nothing
            Dim isNullResult As Boolean = True
            Dim isFoundData As Boolean = False
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                CrystalReportServiceSampleParametersInstance = RetrieveFormInformationFromSession(instance.InstanceFormId)
                UserInfo = New InMotionGIT.Membership.Providers.MemberContext
                formContext = New InMotionGIT.Common.Contracts.Context(InMotionGIT.FASI.Support.Handlers.LanguageHandler.LanguageId(), 
                                                                       instance.InstanceFormId) With {.UserId = HttpContext.Current.Session("UserId"), 
                                                                                                      .UserCode = HttpContext.Current.Session("nUsercode"), 
                                                                                                      .SecuritySchemeCode = HttpContext.Current.Session("sSche_code"), 
                                                                                                      .AccessToken = HttpContext.Current.Session("AccessToken")}
                currentAction = SessionTrace("button2Click")
                CrystalReportServiceSampleParametersInstance = EntryDataToClass(CrystalReportServiceSampleParametersInstance, instance)
currentAction = SessionTrace("1. Call 'Búsqueda de toda la información de un cliente' library")
                Dim _Client_c879f5717a154af88f5625e0cc61de5f As InMotionGIT.Client.Entity.Contracts.Client 
                _Client_c879f5717a154af88f5625e0cc61de5f = (New InMotionGIT.Client.Proxy.Manager).Retrieve(clientId:=CrystalReportServiceSampleParametersInstance.Client.ClientID, atDate:=Date.Today, withLookupInformation:=True, childFilter:="All", accessToken:="String.Empty", provider:="CORE", companyID:=0) 
 
                isNullResult = (IsNothing(_Client_c879f5717a154af88f5625e0cc61de5f)) 
                If Not isNullResult Then
                    CrystalReportServiceSampleParametersInstance.Client = _Client_c879f5717a154af88f5625e0cc61de5f
                End If


                instance = ClassToEntryData(instance, CrystalReportServiceSampleParametersInstance)
                StoreFormInformationOnSession(CrystalReportServiceSampleParametersInstance)
                
                With resultData
                    .Success = True
                    .Data = instance
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "CrystalReportServiceSample", "button2Click", currentAction)
            End Try
            
            Return resultData
        End Function
        <WebMethod(EnableSession:=True)>
        Public Shared Function button3Click(instance As EntryData) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim CrystalReportServiceSampleParametersInstance As CrystalReportServiceSampleParameters = Nothing
            Dim UserInfo As InMotionGIT.Membership.Providers.MemberContext = Nothing
            Dim formContext As InMotionGIT.Common.Contracts.Context = Nothing
            Dim selectDataTableItem As DataTable = Nothing
            Dim currentAction As String = String.Empty
            Dim messageAction As String = String.Empty
            Dim parametersDictionary As Dictionary(Of String, Object) = Nothing
            Dim WorkflowInArguments As Dictionary(Of String, Object) = Nothing
            Dim WorkflowOutArguments As IDictionary(Of String, Object) = Nothing
            Dim isNullResult As Boolean = True
            Dim isFoundData As Boolean = False
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                CrystalReportServiceSampleParametersInstance = RetrieveFormInformationFromSession(instance.InstanceFormId)
                UserInfo = New InMotionGIT.Membership.Providers.MemberContext
                formContext = New InMotionGIT.Common.Contracts.Context(InMotionGIT.FASI.Support.Handlers.LanguageHandler.LanguageId(), 
                                                                       instance.InstanceFormId) With {.UserId = HttpContext.Current.Session("UserId"), 
                                                                                                      .UserCode = HttpContext.Current.Session("nUsercode"), 
                                                                                                      .SecuritySchemeCode = HttpContext.Current.Session("sSche_code"), 
                                                                                                      .AccessToken = HttpContext.Current.Session("AccessToken")}
                currentAction = SessionTrace("button3Click")
                CrystalReportServiceSampleParametersInstance = EntryDataToClass(CrystalReportServiceSampleParametersInstance, instance)
                currentAction = SessionTrace("1. If (CrystalReportServiceSampleParametersInstance.Client.CompleteClientName Is Null Or Empty '') Then")
                If (String.IsNullOrEmpty(CrystalReportServiceSampleParametersInstance.Client.CompleteClientName)) Then

                    currentAction = SessionTrace("2. The message 'You have to retrieve...' will displayed as 'Popup'")
                    ParametersToDictionary(CrystalReportServiceSampleParametersInstance, parametersDictionary)
                    messageAction = "You have to retrieve some client information to generate the report"
                    messageAction = InMotionGIT.FrontOffice.Proxy.Helpers.Email.Process(messageAction, parametersDictionary)
                    resultData.AddNotifyPopup(messageAction)
                    Else
currentAction = SessionTrace("3. Call 'Reporte Client Information sample mapper' library")
                Dim _ContractsCargoCollection_8485634aae5242499941561b84035863 As InMotionGIT.Report.Crystal.Designs.ReportCargo.ContractsCargoCollection 
                _ContractsCargoCollection_8485634aae5242499941561b84035863 = (New InMotionGIT.Report.Crystal.Mappers.ClientInformationMapper).ReportMapping(Client:=CrystalReportServiceSampleParametersInstance.Client, SampleMode:=False, TraceMode:=False) 
 
                isNullResult = (IsNothing(_ContractsCargoCollection_8485634aae5242499941561b84035863)) 
                If Not isNullResult Then
                    CrystalReportServiceSampleParametersInstance.ContractsCargoCollection = _ContractsCargoCollection_8485634aae5242499941561b84035863
                End If

                    currentAction = SessionTrace("4. Call 'Crystal Report Service Sample WF' workflow in Synchronous mode with tracking")
                    WorkflowInArguments = New Dictionary(Of String, Object)

                    With WorkflowInArguments
                    .Add("context", formContext)
                    .Add("result", CrystalReportServiceSampleParametersInstance.result)
                    .Add("cargo", CrystalReportServiceSampleParametersInstance.ContractsCargoCollection)
                    End With

                    WorkflowOutArguments = InMotionGIT.Workflow.Support.Runtime.DoWorkFromForm("CrystalReportSampleWF", "7029c128-56d5-43bb-ac89-d40654a95576", 0, WorkflowInArguments, True, True, HttpContext.Current.Request.UrlReferrer.AbsolutePath, CrystalReportServiceSampleParametersInstance.InternalId)

                    currentAction += "'Mapping Outputs'"
                    CrystalReportServiceSampleParametersInstance.result = WorkflowOutArguments("result")

                        End If

                instance = ClassToEntryData(instance, CrystalReportServiceSampleParametersInstance)
                StoreFormInformationOnSession(CrystalReportServiceSampleParametersInstance)
                
                With resultData
                    .Success = True
                    .Data = instance
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "CrystalReportServiceSample", "button3Click", currentAction)
            End Try
            
            Return resultData
        End Function
        <WebMethod(EnableSession:=True)>
        Public Shared Function button0Click(instance As EntryData) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim CrystalReportServiceSampleParametersInstance As CrystalReportServiceSampleParameters = Nothing
            Dim UserInfo As InMotionGIT.Membership.Providers.MemberContext = Nothing
            Dim formContext As InMotionGIT.Common.Contracts.Context = Nothing
            Dim selectDataTableItem As DataTable = Nothing
            Dim currentAction As String = String.Empty
            Dim messageAction As String = String.Empty
            Dim parametersDictionary As Dictionary(Of String, Object) = Nothing
            Dim WorkflowInArguments As Dictionary(Of String, Object) = Nothing
            Dim WorkflowOutArguments As IDictionary(Of String, Object) = Nothing
            Dim isNullResult As Boolean = True
            Dim isFoundData As Boolean = False
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                CrystalReportServiceSampleParametersInstance = RetrieveFormInformationFromSession(instance.InstanceFormId)
                UserInfo = New InMotionGIT.Membership.Providers.MemberContext
                formContext = New InMotionGIT.Common.Contracts.Context(InMotionGIT.FASI.Support.Handlers.LanguageHandler.LanguageId(), 
                                                                       instance.InstanceFormId) With {.UserId = HttpContext.Current.Session("UserId"), 
                                                                                                      .UserCode = HttpContext.Current.Session("nUsercode"), 
                                                                                                      .SecuritySchemeCode = HttpContext.Current.Session("sSche_code"), 
                                                                                                      .AccessToken = HttpContext.Current.Session("AccessToken")}
                currentAction = SessionTrace("button0Click")
                CrystalReportServiceSampleParametersInstance = EntryDataToClass(CrystalReportServiceSampleParametersInstance, instance)
                currentAction = SessionTrace("1. Url: 'CrystalReportServiceSampleParametersInstance.result'")
                    
                With resultData
                    .AddRedirect(String.Format(CultureInfo.InvariantCulture,CrystalReportServiceSampleParametersInstance.result, ""), "scrollbars=no,resizable=no,toolbar=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=50,height=50,left=0,top=0")
                End With

                instance = ClassToEntryData(instance, CrystalReportServiceSampleParametersInstance)
                StoreFormInformationOnSession(CrystalReportServiceSampleParametersInstance)
                
                With resultData
                    .Success = True
                    .Data = instance
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "CrystalReportServiceSample", "button0Click", currentAction)
            End Try
            
            Return resultData
        End Function

#End Region

#Region "Lookups Web Methods"


#End Region

#Region "Form Actions"


#End Region
#Region "Utilities"

        Private Shared Sub ParametersToDictionary(formData As CrystalReportServiceSampleParameters, ByRef target As Dictionary(Of String, Object))
            If IsNothing(target) Then
                target = New Dictionary(Of String, Object)

                target.Add("Client", formData.Client)
                target.Add("result", formData.result)
                target.Add("ContractsCargoCollection", formData.ContractsCargoCollection)
            Else
                target("Client") = formData.Client
                target("result") = formData.result
                target("ContractsCargoCollection") = formData.ContractsCargoCollection
            End If
        End Sub

        Private Shared Sub GetTransferParameters(parametersData As CrystalReportServiceSampleParameters, fromid As String)
            Dim fromDocumentCache As Object = Nothing

            If Not String.IsNullOrEmpty(fromid) Then
                fromDocumentCache = HttpContext.Current.Session(fromid)
                HttpContext.Current.Session.Remove(fromid)

            Else
                fromDocumentCache = HttpContext.Current.Session(HttpContext.Current.Session("fromid"))
                HttpContext.Current.Session.Remove(HttpContext.Current.Session("fromid"))
                HttpContext.Current.Session.Remove("fromid")
            End If

            If Not IsNothing(fromDocumentCache) Then
                InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("Client", fromDocumentCache, parametersData)
                InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("result", fromDocumentCache, parametersData)
                InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("ContractsCargoCollection", fromDocumentCache, parametersData)
            End If
        End Sub

        Private Shared Function SessionTrace(message As String) As String

            If Not IsNothing(HttpContext.Current.Session("Form.Track")) AndAlso HttpContext.Current.Session("Form.Track").ToString.ToLower = "true" Then
                Dim tracelog As String = HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension("Page.AppRelativeVirtualPath")))

                tracelog += String.Format(CultureInfo.InvariantCulture, "{0} {1}<br>{2}", Now.ToString("hh:mm:ss.fff"), message, vbCrLf)
                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension("Page.AppRelativeVirtualPath"))) = tracelog
            End If

            Return message
        End Function

#End Region
#Region "Storage Session Methods"

        Private Shared Function RetrieveFormInformationFromSession(id As String) As CrystalReportServiceSampleParameters
            
            Dim source As String = HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.Storage.{0}", id))
            Dim instance As CrystalReportServiceSampleParameters = Nothing

            If Not String.IsNullOrEmpty(source) Then
                instance = Serialize.Deserialize(Of CrystalReportServiceSampleParameters)(source)
            End If
            
            Return instance
        End Function

        Private Shared Sub StoreFormInformationOnSession(instance As CrystalReportServiceSampleParameters)
            
            If Not IsNothing(instance) Then
                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.Storage.{0}", instance.InternalId)) = Serialize.Serialize(Of CrystalReportServiceSampleParameters)(instance)
                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.Title.{0}", instance.InternalId)) = "Client information report"
              
                If Not IsNothing(HttpContext.Current.Session("Form.Track")) AndAlso
                   HttpContext.Current.Session("Form.Track").ToString.ToLower = "true" Then

                    HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.CrystalReportServiceSample", instance.InternalId)) = instance
                End If

                If Not IsNothing(HttpContext.Current.Session("Form.Track.Parameters")) AndAlso
                   HttpContext.Current.Session("Form.Track.Parameters").ToString.ToLower = "true" Then

                    If Not IsNothing(HttpContext.Current.Session("Form.Track")) AndAlso HttpContext.Current.Session("Form.Track").ToString.ToLower = "true" Then
                        InMotionGIT.Common.Helpers.FileHandler.SaveContent(String.Format(CultureInfo.InvariantCulture, "{0}\{1}.CrystalReportServiceSample.txt",
                                                                                         ConfigurationManager.AppSettings("Path.Logs"), instance.InternalId),
                                                                                          HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture,
                                                                                                                     "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension("Page.AppRelativeVirtualPath"))))
                    End If

                    InMotionGIT.Common.Helpers.Serialize.SerializeToFile(Of CrystalReportServiceSampleParameters)(instance,
                                                                                                String.Format(CultureInfo.InvariantCulture,
                                                                                                              "{0}\{1}.CrystalReportServiceSample.xml", ConfigurationManager.AppSettings("Path.Logs"), instance.InternalId), True)
                End If			
            End If
            
        End Sub

#End Region


#Region "Contracts Mappers"

        Private Shared Function EntryDataToClass(parametersData As CrystalReportServiceSampleParameters, formData As EntryData) As CrystalReportServiceSampleParameters
            If IsNothing(parametersData) Then
                parametersData = New CrystalReportServiceSampleParameters
            End If

            If IsNothing(parametersData.Client) Then
                parametersData.Client = New InMotionGIT.Client.Entity.Contracts.Client
            End If
            If IsNothing(parametersData.Client.ClientHobbies) Then
                parametersData.Client.ClientHobbies = New InMotionGIT.Client.Entity.Contracts.ClientHobbyCollection
            End If
            If IsNothing(parametersData.Client.ClientSports) Then
                parametersData.Client.ClientSports = New InMotionGIT.Client.Entity.Contracts.ClientSportCollection
            End If

            With parametersData
                .InternalId = formData.InstanceFormId
                .Client.ClientID = formData.ClientClientID
                 If formData.Hobbies_ClientHobby.IsNotEmpty Then
                    Dim temporalClientHobbies As New InMotionGIT.Client.Entity.Contracts.ClientHobbyCollection
                    Dim currentItem As InMotionGIT.Client.Entity.Contracts.ClientHobby

	            For Each itemData As Hobbies_ClientHobbyItem In formData.Hobbies_ClientHobby
                        currentItem = (From _x In parametersData.Client.ClientHobbies Where _x.Hobby = itemData.Hobby Select _x).FirstOrDefault()
                     
	                If currentItem.IsEmpty Then
                            currentItem = New InMotionGIT.Client.Entity.Contracts.ClientHobby
                  
                            With currentItem

                                .Hobby = itemData.Hobby
                                .HobbyDescription = itemData.HobbyDescription
                            End With                      
                                              
	                Else
                            With currentItem

                                .Hobby = itemData.Hobby
                                .HobbyDescription = itemData.HobbyDescription
                            End With
                        End If
                        
                        temporalClientHobbies.Add(currentItem)
                    Next
                    
                    parametersData.Client.ClientHobbies = temporalClientHobbies
                 End If
                 If formData.Sports_ClientSport.IsNotEmpty Then
                    Dim temporalClientSports As New InMotionGIT.Client.Entity.Contracts.ClientSportCollection
                    Dim currentItem As InMotionGIT.Client.Entity.Contracts.ClientSport

	            For Each itemData As Sports_ClientSportItem In formData.Sports_ClientSport
                        currentItem = (From _x In parametersData.Client.ClientSports Where _x.Sport = itemData.Sport Select _x).FirstOrDefault()
                     
	                If currentItem.IsEmpty Then
                            currentItem = New InMotionGIT.Client.Entity.Contracts.ClientSport
                  
                            With currentItem

                                .Sport = itemData.Sport
                                .SportDescription = itemData.SportDescription
                            End With                      
                                              
	                Else
                            With currentItem

                                .Sport = itemData.Sport
                                .SportDescription = itemData.SportDescription
                            End With
                        End If
                        
                        temporalClientSports.Add(currentItem)
                    Next
                    
                    parametersData.Client.ClientSports = temporalClientSports
                 End If
            End With

            Return parametersData
        End Function

        Private Shared Function ClassToEntryData(formData As EntryData, parametersData As CrystalReportServiceSampleParameters) As EntryData
            formData = New EntryData

            With formData
                .InstanceFormId = parametersData.InternalId
                If Not IsNothing(parametersData.Client) Then
                    .ClientClientID = parametersData.Client.ClientID
                End If
                If Not IsNothing(parametersData.Client) Then
                    .ClientCompleteClientName = parametersData.Client.CompleteClientName
                End If
                .Hobbies_ClientHobby = New List(Of Hobbies_ClientHobbyItem)

                If Not IsNothing(parametersData.Client) AndAlso Not IsNothing(parametersData.Client.ClientHobbies) Then
                    Dim newItem As Hobbies_ClientHobbyItem
                    
                    For Each itemData As InMotionGIT.Client.Entity.Contracts.ClientHobby In parametersData.Client.ClientHobbies
                        newItem = New Hobbies_ClientHobbyItem
                        
                        With newItem
                            .Hobby = itemData.Hobby
                            .HobbyDescription = itemData.HobbyDescription                        
                        End With

                        .Hobbies_ClientHobby.Add(newItem)
                    Next

                    If .Hobbies_ClientHobby.Count <> 0 Then

                    End If
                End If


                .Sports_ClientSport = New List(Of Sports_ClientSportItem)

                If Not IsNothing(parametersData.Client) AndAlso Not IsNothing(parametersData.Client.ClientSports) Then
                    Dim newItem As Sports_ClientSportItem
                    
                    For Each itemData As InMotionGIT.Client.Entity.Contracts.ClientSport In parametersData.Client.ClientSports
                        newItem = New Sports_ClientSportItem
                        
                        With newItem
                            .Sport = itemData.Sport
                            .SportDescription = itemData.SportDescription                        
                        End With

                        .Sports_ClientSport.Add(newItem)
                    Next

                    If .Sports_ClientSport.Count <> 0 Then

                    End If
                End If


                .result = parametersData.result
            End With

            Return formData
        End Function

#End Region

#Region "Common Utilities"

        Private Shared Sub SetDefaultValuesFromQueryString(formData As CrystalReportServiceSampleParameters)
            If HttpContext.Current.Request.QueryString("result").IsNotEmpty Then
                formData.result = HttpContext.Current.Request.QueryString("result")
            End If
        End Sub	

        Private Shared Sub ValidateParametersInstance(ByRef formData As CrystalReportServiceSampleParameters)
            If IsNothing(formData.Client) Then
                formData.Client = New InMotionGIT.Client.Entity.Contracts.Client
            End If
            If IsNothing(formData.Client.ClientHobbies) Then
                formData.Client.ClientHobbies = New InMotionGIT.Client.Entity.Contracts.ClientHobbyCollection
            End If
            If IsNothing(formData.Client.ClientSports) Then
                formData.Client.ClientSports = New InMotionGIT.Client.Entity.Contracts.ClientSportCollection
            End If

        End Sub

#End Region

#Region "Form Contracts"

        <Serializable()>
        <DataContract()>
        Public Class EntryData

            <DataMember()> Public Property InstanceFormId As String
            <DataMember()> Public Property ClientClientID As System.String
            <DataMember()> Public Property ClientCompleteClientName As System.String
            <DataMember()> Public Property ClientClientHobbiesHobby As System.Int32
            <DataMember()> Public Property ClientClientHobbiesHobbyDescription As System.String
            <DataMember()> Public Property Hobbies_ClientHobby As List(Of Hobbies_ClientHobbyItem)
            <DataMember()> Public Property ClientClientSportsSport As System.Int32
            <DataMember()> Public Property ClientClientSportsSportDescription As System.String
            <DataMember()> Public Property Sports_ClientSport As List(Of Sports_ClientSportItem)
            <DataMember()> Public Property result As String

        End Class

        <Serializable()>
        <DataContract()>
        Public Class Hobbies_ClientHobbyItem

            <DataMember()> Public Property Hobby As System.Int32
            <DataMember()> Public Property HobbyDescription As System.String

        End Class

        <Serializable()>
        <DataContract()>
        Public Class Sports_ClientSportItem

            <DataMember()> Public Property Sport As System.Int32
            <DataMember()> Public Property SportDescription As System.String

        End Class

        <Serializable()>
        Public Class CrystalReportServiceSampleParameters
            Inherits InMotionGIT.FrontOffice.Support.DataType.FormBase(Of CrystalReportServiceSampleParameters)

            Public Property Client As InMotionGIT.Client.Entity.Contracts.Client
            Public Property result As String
            Public Property ContractsCargoCollection As InMotionGIT.Report.Crystal.Designs.ReportCargo.ContractsCargoCollection

        End Class
#End Region

    End Class

End Namespace

