'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer v7.3.45.1 at 2020-06-01 10:18:20 a. m. model release 28, Form Generator v1.0.37.70
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

    Public Class NewAccountActions
        Inherits System.Web.UI.Page

#Region "Actions Methods"

       <WebMethod(EnableSession:=True)>
        Public Shared Function Initialization(id As String, urlid As String, fromid As String) As InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            
            Dim instance As New EntryData With {.InstanceFormId = id}
            Dim resultData As New InMotionGIT.FrontOffice.Support.DataType.ServerActionResult
            Dim currentAction As String = String.Empty
            
            Dim NewAccountParametersInstance As NewAccountParameters = Nothing

            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                
                If id.IsEmpty AndAlso urlid.IsEmpty Then
                    instance.InstanceFormId = System.Guid.NewGuid().ToString
                    
                    With instance

                    End With                
                    
                    NewAccountParametersInstance = EntryDataToClass(NewAccountParametersInstance, instance)
                    
                    

                Else       
                    instance.InstanceFormId = id.IfEmpty(urlid)
                    NewAccountParametersInstance = RetrieveFormInformationFromSession(instance.InstanceFormId)
         
                    If IsNothing(NewAccountParametersInstance) then
                        NewAccountParametersInstance = EntryDataToClass(NewAccountParametersInstance, instance)                        
                    End If
                    
                    
                End If

                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension("Page.AppRelativeVirtualPath"))) = String.Empty



                instance = ClassToEntryData(instance, NewAccountParametersInstance) 
		        StoreFormInformationOnSession(NewAccountParametersInstance)
                
                                
                With resultData
                    .Success = True
                    .Data = New With {.Instance = instance, .LookUps = LoadLookupsList(NewAccountParametersInstance)}
                End With

            Catch ex As Exception
                resultData = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessServerAction(ex, "NewAccount", "Initialization", currentAction)
            End Try
            
            Return resultData
        End Function
  

        Public Shared Function LoadLookupsList(NewAccountParametersInstance As NewAccountParameters) As List(Of InMotionGIT.Common.DataType.LookUpPackage)
            Dim result As New List(Of InMotionGIT.Common.DataType.LookUpPackage)
            
            Dim dataFactory As New DataManagerFactory("PackageExecuteToLookUp", "LOOKUPS", "")
            Dim dataCommand As InMotionGIT.Common.Services.Contracts.DataCommand = Nothing
            
            dataCommand = dataFactory.AddCommand(String.Format(CultureInfo.CurrentCulture, "SELECT  LOOKUP.LANGUAGEID, LOOKUP.DESCRIPTION, LOOKUP.CODE, LOOKUP.RECORDSTATUS, LOOKUP.LOOKUPID FROM LOOKUP LOOKUP  WHERE LOOKUP.LOOKUPID = {0} AND LOOKUP.RECORDSTATUS = {1} AND LOOKUP.LANGUAGEID = {2}  AND NOT LOOKUP.DESCRIPTION IS NULL ORDER BY DESCRIPTION", 1, 1, InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId()), New InMotionGIT.Common.DataType.LookUpValue With {.Code = "CODE", .Description = "DESCRIPTION"}, "LanguageID", "LOOKUP", "Linked.FrontOffice")

            dataCommand = dataFactory.AddCommand(String.Format(CultureInfo.CurrentCulture, "SELECT  TABLE66.NCOUNTRY, TRIM(TABLE66.SDESCRIPT) SDESCRIPT FROM TABLE66 TABLE66  WHERE TABLE66.SSTATREGT = 1  ORDER BY TABLE66.SDESCRIPT ASC ", "1"), New InMotionGIT.Common.DataType.LookUpValue With {.Code = "NCOUNTRY", .Description = "SDESCRIPT"}, "Country", "TABLE66", "Linked.LatCombined")
            
            result = dataFactory.PackageExecuteToLookUp()
	
            Return result
        End Function




#End Region

#Region "Lookups Web Methods"


        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForLanguageID(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)                       
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                Dim NewAccountParametersInstance As NewAccountParameters = RetrieveFormInformationFromSession(id) 
                Dim UserInfo As New InMotionGIT.Membership.Providers.MemberContext
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  LOOKUP.LANGUAGEID, LOOKUP.DESCRIPTION, LOOKUP.CODE, LOOKUP.RECORDSTATUS, LOOKUP.LOOKUPID FROM LOOKUP LOOKUP  WHERE LOOKUP.LOOKUPID = {0} AND LOOKUP.RECORDSTATUS = {1} AND LOOKUP.LANGUAGEID = {2}  AND NOT LOOKUP.DESCRIPTION IS NULL ORDER BY DESCRIPTION", 1, 1, InMotionGIT.FASI.Support.Handlers.LanguageHandler.ContextLanguageId()), 
                                            "LOOKUP", "Linked.FrontOffice")

                    
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.NumericValue("CODE"),
                                                                                            .Description = item.StringValue("DESCRIPTION")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "NewAccount", "LookUpForLanguageID", String.Empty)
            End Try
                        
            Return result
        End Function

        <WebMethod(EnableSession:=True)>
        Public Shared Function LookUpForCountry(id As String) As InMotionGIT.FrontOffice.Support.DataType.ClientActionResult
            
            Dim result As New InMotionGIT.FrontOffice.Support.DataType.ClientActionResult With {.Success = True}
            Dim resultData As DataTable = Nothing
            Dim newLookupList As New List(Of InMotionGIT.Common.DataType.LookUpValue)                       
            
            Try
                InMotionGIT.FASI.Support.Authentication.AuthorizationProcess("*")
                Dim NewAccountParametersInstance As NewAccountParameters = RetrieveFormInformationFromSession(id) 
                
                With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture,
                                                          "SELECT  TABLE66.NCOUNTRY, TRIM(TABLE66.SDESCRIPT) SDESCRIPT FROM TABLE66 TABLE66  WHERE TABLE66.SSTATREGT = 1  ORDER BY TABLE66.SDESCRIPT ASC ", "1"), 
                                            "TABLE66", "Linked.LatCombined")

                    .Cache = InMotionGIT.Common.Enumerations.EnumCache.CacheWithFullParameters
                    resultData = .QueryExecuteToTable(True)
                End With

                If Not IsNothing(resultData) Then
                    For Each item As DataRow In resultData.Rows
                        newLookupList.Add(New InMotionGIT.Common.DataType.LookUpValue With {.Code = item.NumericValue("NCOUNTRY"),
                                                                                            .Description = item.StringValue("SDESCRIPT")})
                    Next
                End If

                result.Data = newLookupList

            Catch ex As Exception
                result = InMotionGIT.FrontOffice.Support.ExceptionHandler.ProcessClientAction(ex, "NewAccount", "LookUpForCountry", String.Empty)
            End Try
                        
            Return result
        End Function

#End Region

#Region "Form Actions"


#End Region
#Region "Utilities"


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

        Private Shared Function RetrieveFormInformationFromSession(id As String) As NewAccountParameters
            
            Dim source As String = HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.Storage.{0}", id))
            Dim instance As NewAccountParameters = Nothing

            If Not String.IsNullOrEmpty(source) Then
                instance = Serialize.Deserialize(Of NewAccountParameters)(source)
            End If
            
            Return instance
        End Function

        Private Shared Sub StoreFormInformationOnSession(instance As NewAccountParameters)
            
            If Not IsNothing(instance) Then
                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.Storage.{0}", instance.InternalId)) = Serialize.Serialize(Of NewAccountParameters)(instance)
                HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.Title.{0}", instance.InternalId)) = "Registro de usuario"
              
                If Not IsNothing(HttpContext.Current.Session("Form.Track")) AndAlso
                   HttpContext.Current.Session("Form.Track").ToString.ToLower = "true" Then

                    HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture, "Form.NewAccount", instance.InternalId)) = instance
                End If

                If Not IsNothing(HttpContext.Current.Session("Form.Track.Parameters")) AndAlso
                   HttpContext.Current.Session("Form.Track.Parameters").ToString.ToLower = "true" Then

                    If Not IsNothing(HttpContext.Current.Session("Form.Track")) AndAlso HttpContext.Current.Session("Form.Track").ToString.ToLower = "true" Then
                        InMotionGIT.Common.Helpers.FileHandler.SaveContent(String.Format(CultureInfo.InvariantCulture, "{0}\{1}.NewAccount.txt",
                                                                                         ConfigurationManager.AppSettings("Path.Logs"), instance.InternalId),
                                                                                          HttpContext.Current.Session(String.Format(CultureInfo.InvariantCulture,
                                                                                                                     "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension("Page.AppRelativeVirtualPath"))))
                    End If

                    InMotionGIT.Common.Helpers.Serialize.SerializeToFile(Of NewAccountParameters)(instance,
                                                                                                String.Format(CultureInfo.InvariantCulture,
                                                                                                              "{0}\{1}.NewAccount.xml", ConfigurationManager.AppSettings("Path.Logs"), instance.InternalId), True)
                End If			
            End If
            
        End Sub

#End Region


#Region "Contracts Mappers"

        Private Shared Function EntryDataToClass(parametersData As NewAccountParameters, formData As EntryData) As NewAccountParameters
            If IsNothing(parametersData) Then
                parametersData = New NewAccountParameters
            End If

            If IsNothing(parametersData.UserInformation) Then
                parametersData.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
            End If

            With parametersData
                .InternalId = formData.InstanceFormId
                .userType = formData.userType
                .UserInformation.UserName = formData.UserInformationUserName
                .UserInformation.Email = formData.UserInformationEmail
                .EmailVerification = formData.EmailVerification
                .UserInformation.LanguageID = formData.UserInformationLanguageID
                .UserInformation.FirstName = formData.UserInformationFirstName
                .UserInformation.SurName = formData.UserInformationSurName
                .UserInformation.LastName = formData.UserInformationLastName
                .UserInformation.SecondLastName = formData.UserInformationSecondLastName
                .UserInformation.DateOfBirth = formData.UserInformationDateOfBirth
                .UserInformation.Gender = formData.UserInformationGender
                .UserInformation.AddressHome = formData.UserInformationAddressHome
                .UserInformation.Country = formData.UserInformationCountry
                .UserInformation.City = formData.UserInformationCity
                .UserInformation.State = formData.UserInformationState
                .UserInformation.TelephoneNumber = formData.UserInformationTelephoneNumber
                .identificatorAgent = formData.identificatorAgent
                .identificatorClient = formData.identificatorClient
                .AgreeTerms = formData.AgreeTerms
            End With

            Return parametersData
        End Function

        Private Shared Function ClassToEntryData(formData As EntryData, parametersData As NewAccountParameters) As EntryData
            formData = New EntryData

            With formData
                .InstanceFormId = parametersData.InternalId
                .userType = parametersData.userType
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationUserName = parametersData.UserInformation.UserName
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationEmail = parametersData.UserInformation.Email
                End If
                .EmailVerification = parametersData.EmailVerification
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationLanguageID = parametersData.UserInformation.LanguageID
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationFirstName = parametersData.UserInformation.FirstName
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationSurName = parametersData.UserInformation.SurName
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationLastName = parametersData.UserInformation.LastName
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationSecondLastName = parametersData.UserInformation.SecondLastName
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationDateOfBirth = parametersData.UserInformation.DateOfBirth
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationGender = parametersData.UserInformation.Gender
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationAddressHome = parametersData.UserInformation.AddressHome
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationCountry = parametersData.UserInformation.Country
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationCity = parametersData.UserInformation.City
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationState = parametersData.UserInformation.State
                End If
                If Not IsNothing(parametersData.UserInformation) Then
                    .UserInformationTelephoneNumber = parametersData.UserInformation.TelephoneNumber
                End If
                .identificatorAgent = parametersData.identificatorAgent
                .identificatorClient = parametersData.identificatorClient
                .AgreeTerms = parametersData.AgreeTerms
            End With

            Return formData
        End Function

#End Region

#Region "Common Utilities"

        Private Shared Sub ValidateParametersInstance(ByRef formData As NewAccountParameters)
            If IsNothing(formData.UserInformation) Then
                formData.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
            End If

        End Sub

#End Region

#Region "Form Contracts"

        <Serializable()>
        <DataContract()>
        Public Class EntryData

            <DataMember()> Public Property InstanceFormId As String
            <DataMember()> Public Property userType As String
            <DataMember()> Public Property UserInformationUserName As System.String
            <DataMember()> Public Property UserInformationEmail As System.String
            <DataMember()> Public Property EmailVerification As String
            <DataMember()> Public Property UserInformationLanguageID As System.Int32
            <DataMember()> Public Property UserInformationFirstName As System.String
            <DataMember()> Public Property UserInformationSurName As System.String
            <DataMember()> Public Property UserInformationLastName As System.String
            <DataMember()> Public Property UserInformationSecondLastName As System.String
            <DataMember()> Public Property UserInformationDateOfBirth As System.DateTime
            <DataMember()> Public Property UserInformationGender As System.String
            <DataMember()> Public Property UserInformationAddressHome As System.String
            <DataMember()> Public Property UserInformationCountry As System.String
            <DataMember()> Public Property UserInformationCity As System.String
            <DataMember()> Public Property UserInformationState As System.String
            <DataMember()> Public Property UserInformationTelephoneNumber As System.Int32
            <DataMember()> Public Property identificatorAgent As Int64
            <DataMember()> Public Property identificatorClient As String
            <DataMember()> Public Property AgreeTerms As Boolean

        End Class

        <Serializable()>
        Public Class NewAccountParameters
            Inherits InMotionGIT.FrontOffice.Support.DataType.FormBase(Of NewAccountParameters)

            Public Property UserInformation As InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
            Public Property EmailVerification As String
            Public Property AgreeTerms As Boolean
            Public Property TemporalUserName As String
            Public Property identificatorAgent As Int64
            Public Property identificatorClient As String
            Public Property Parameter8 As String
            Public Property UserDTO As InMotionGIT.FASI.Support.Entities.UserDTO
            Public Property userType As String

        End Class
#End Region

    End Class

End Namespace

