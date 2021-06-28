﻿'---------------------------------------------------------------------------------------------------
' <generated>
'     This code was generated by Form Designer Oracle v7.1.1 at 2017/11/17 model release 17
'     
'     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
' </generated>
'---------------------------------------------------------------------------------------------------
      
#Region "using"
    
Imports Artem.Google.UI
Imports DashboardBusiness.Helpers
Imports DevExpress.Web
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView
Imports GIT.Core
Imports InMotionGIT.FrontOffice.Support.Enumerations
Imports InMotionGIT.FrontOffice.Support.Helpers.ControlHandler
Imports InMotionGIT.BarCode
Imports InMotionGIT.BarCode.Enumerations
Imports InMotionGIT.Common.Helpers
Imports InMotionGIT.Common.Proxy
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.DTOs
Imports InMotionGIT.DatosNoEstruct.ContratoDeDatos.Modelo
Imports System.IO
Imports InMotionGIT.FrontOffice.Support
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.Common
Imports System.Globalization
Imports System.Linq
Imports System.Threading.Thread
Imports System.Xml.Linq
Imports System.Net


#End Region

Partial Public Class UserRegistrationUserControl
    Inherits GIT.Core.UserControlBase
    Implements Dropthings.Widget.Framework.IWidget

#Region "IWidget Members"
    
    
    

    Public Sub Closed() Implements Dropthings.Widget.Framework.IWidget.Closed
    End Sub

    Public Sub HideSettings() Implements Dropthings.Widget.Framework.IWidget.HideSettings

    End Sub

    Public Sub Init1(host As Dropthings.Widget.Framework.IWidgetHost) Implements Dropthings.Widget.Framework.IWidget.Init

    End Sub

    Public Sub Maximized() Implements Dropthings.Widget.Framework.IWidget.Maximized

    End Sub

    Public Sub Minimized() Implements Dropthings.Widget.Framework.IWidget.Minimized

    End Sub

    Public Sub ShowSettings() Implements Dropthings.Widget.Framework.IWidget.ShowSettings

    End Sub

#End Region

#Region "Private fields"

    Private _formData As UserRegistrationParameter = Nothing
    Private _CurrentParameterInstance As Boolean = False
    Private _loading As Boolean = False
    Private _loadcompleted As Boolean = False
    Private _webTransfer as String  
    
        
#End Region

#Region "Public properties"

    Public Property FormData() As UserRegistrationParameter
        Get
            Dim formDefinition As String = String.Empty               
             
            If IsNothing(_formData) Then
                If Not IsPostBack Then
                    If Not String.IsNullOrEmpty(Request.QueryString("id")) Then
                        Dim _id As Guid
                        
                        If Guid.TryParse(Request.QueryString("id"), _id) Then
		                        _FormID.Text = Request.QueryString("id")  
                            
                            formDefinition = Session(String.Format(CultureInfo.InvariantCulture, "FormStorage.{0}", _FormID.Text))
    
           If Not String.IsNullOrEmpty(formDefinition) Then
              _formData = Serialize.Deserialize(Of UserRegistrationParameter)(formDefinition)
           End If
                        End If
                    End If

                    If IsNothing(_formData) Then
                        _formData = New UserRegistrationParameter
                       
                        If String.IsNullOrEmpty(_FormID.Text) Then
                        	_FormID.Text = System.Guid.NewGuid().ToString                       
                        End If
											
                        _formData.InternalId = _FormID.Text
                        
                        If Not String.IsNullOrEmpty(Request.QueryString("fromid")) OrElse
                           Not String.IsNullOrEmpty(Session("fromid")) Then
                           
                            GetTransferParameters()
                        End If
                        
                        
                        
                        SetDefaultValuesFromQueryString()
                        ValidateParametersInstance(_formData)
                        
                        popupNotifyMessage.ShowOnPageLoad = false                        
                        
                        ControlsInitialization()
                        ExecuteActionsInitializationForm()
                         
                        If Not String.IsNullOrEmpty(Request.QueryString("readonly")) Then
                            SetReadOnlyControls()
                        End If
                        
                        If Not String.IsNullOrEmpty(Request.QueryString("btnRegister")) Then
                            btnRegister_Click(nothing, nothing)
                        End If

                    Else
                    		SetDefaultValuesFromQueryString()
                        
                    End If

                Else
                     formDefinition = Session(String.Format(CultureInfo.InvariantCulture, "FormStorage.{0}", _FormID.Text))
    
           If Not String.IsNullOrEmpty(formDefinition) Then
              _formData = Serialize.Deserialize(Of UserRegistrationParameter)(formDefinition)
           End If                    

                    If IsNothing(_formData) Then
                        _formData = New UserRegistrationParameter
                    End If
                End If
            End If

            Return _formData
        End Get

        Set(value As UserRegistrationParameter)
            _formData = value
        End Set
    End Property
    
#End Region

#Region "Form Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ErrorsGridView.Visible = False
        
        
        
        Dim formData As UserRegistrationParameter = Me.FormData        
           
        If Not IsPostBack  AndAlso Not GridViewPostBack() Then                     
            
            
            _loadcompleted = True
            
            
            If IsPostBack And Not _CurrentParameterInstance Then
                MapPageToClass(formData)
                _CurrentParameterInstance = True
            End If

            MapClassToPage(formData)
            VerifySecurityLevel()
            
            
        Else
            
            
            
        End If        
        
        
        
        
    End Sub    

    Protected Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload        
        If Not IsNothing(_formData) Then  
                       Session(String.Format(CultureInfo.InvariantCulture, "FormStorage.{0}", _FormID.Text)) = Serialize.Serialize(Of UserRegistrationParameter)(_formData)
                       Session(String.Format(CultureInfo.InvariantCulture, "FormTitle.{0}", _FormID.Text)) = Page.Title
        End If 
        
        If Not IsNothing(_formData) Then
             If Not IsNothing(Session("Form.Track")) AndAlso Session("Form.Track").ToString.ToLower = "true" Then
                 Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}", IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath))) = _formData
             End If   
       
             If Not IsNothing(Session("Form.Track.Parameters")) AndAlso Session("Form.Track.Parameters").ToString.ToLower = "true" Then
                  InMotionGIT.Common.Helpers.Serialize.SerializeToFile(Of UserRegistrationParameter) _
                  (formData, String.Format(CultureInfo.InvariantCulture, "{0}\{1}.UserRegistration.xml",  ConfigurationManager.AppSettings("Path.Logs"), formData.InternalId), True)
             End If        
        End If  
   End Sub

#End Region

#Region "Controls Events"


    Private Function GridViewPostBack() As Boolean
        For index As Integer = 0 To Request.Params.Count - 1
            If Request.Params(index).EndsWith("$")  Then
                Return True
            End If
        Next
        
        Return False
    End Function    
        
    Protected Sub Country_DataBinding(sender As Object, e As EventArgs) Handles Country.DataBinding
	Dim source As DataTable = Nothing

	With New DataManagerFactory(String.Format(CultureInfo.CurrentCulture, 
											  "SELECT  TABLE66.NCOUNTRY, TRIM(TABLE66.SDESCRIPT) SDESCRIPT FROM TABLE66 TABLE66  WHERE TABLE66.SSTATREGT = 1  ORDER BY TABLE66.SDESCRIPT ASC ", "1"), "TABLE66", "Linked.LatCombined")

		
		.Cache = InMotionGIT.Common.Enumerations.EnumCache.CacheWithFullParameters
		source = .QueryExecuteToTable(True)
		Country.DataSource = source
	End With
End Sub

    
#End Region


#Region "PopupMenu Events"

    
    
#End Region

#Region "Mapping methods"

    Public Sub ValidateParametersInstance(ByRef UserRegistrationParameterInstance As UserRegistrationParameter)
        If IsNothing(UserRegistrationParameterInstance.UserInformation) Then
            UserRegistrationParameterInstance.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
        End If
   
    End Sub
    
    ''' <summary>
    ''' This method moves the data from the class to the page
    ''' </summary>
    ''' <param name="UserRegistrationParameterInstance"></param>
    ''' <remarks></remarks>
    Public Sub MapClassToPage(ByRef UserRegistrationParameterInstance As UserRegistrationParameter, Optional calledBy As String = "")
        Dim parametersDictionary As Dictionary(Of String, Object) = Nothing
        ValidateParametersInstance(UserRegistrationParameterInstance)

        Gender.DataBind()
Country.DataBind()

       
        With UserRegistrationParameterInstance
          
                      UserName.Text = .UserInformation.UserName
            Email.Text = .UserInformation.Email
            EmailVerification.Text = .EmailVerification
            SecurityQuestion.Text = .UserInformation.PasswordQuestion
            SecurityAnswer.Text = .UserInformation.PasswordAnswer
            FirstName.Text = .UserInformation.FirstName
            SurName.Text = .UserInformation.SurName
            LastName.Text = .UserInformation.LastName
            SecondLastName.Text = .UserInformation.SecondLastName
            DateOfBirth.Value = .UserInformation.DateOfBirth
            Gender.SelectedItem = Gender.Items.FindByValue(.UserInformation.Gender)
            AddressHome.Text = .UserInformation.AddressHome
            City.Text = .UserInformation.City
            State.Text = .UserInformation.State
            Country.SelectedItem = Country.Items.FindByValue(.UserInformation.Country)
            AreaNumber.Text = .UserInformation.AreaNumber
            TelephoneNumber.Text = .UserInformation.TelephoneNumber
            ExtensionNumber.Text = .UserInformation.ExtensionNumber
            text1.Text = .identificatorAgent
            text3.Text = .identificatorClient
            AcceptConditions.Checked = .AgreeTerms

              
                  
           
 
          
               
            
            
       End With


if (Not ("".IndexOf(calledBy) > -1)) Or String.IsNullOrEmpty(calledBy) then        
            
         

        End If          
    End Sub

    ''' <summary>
    ''' This method moves the data from the page to the class
    ''' </summary>
    ''' <param name="UserRegistrationParameterInstance"></param>
    Public Sub MapPageToClass(ByRef UserRegistrationParameterInstance As UserRegistrationParameter)
        _loading = True

        ValidateParametersInstance(UserRegistrationParameterInstance)

        With UserRegistrationParameterInstance
            .UserInformation.UserName = UserName.Text
            .UserInformation.Email = Email.Text
            .EmailVerification = EmailVerification.Text
            .UserInformation.PasswordQuestion = SecurityQuestion.Text
            .UserInformation.PasswordAnswer = SecurityAnswer.Text
            .UserInformation.FirstName = FirstName.Text
            .UserInformation.SurName = SurName.Text
            .UserInformation.LastName = LastName.Text
            .UserInformation.SecondLastName = SecondLastName.Text
            .UserInformation.DateOfBirth = DateOfBirth.Value
            .UserInformation.Gender = Gender.Value
            .UserInformation.AddressHome = AddressHome.Text
            .UserInformation.City = City.Text
            .UserInformation.State = State.Text
            .UserInformation.Country = Country.Value
            .UserInformation.AreaNumber = AreaNumber.Text
            .UserInformation.TelephoneNumber = TelephoneNumber.Text
            .UserInformation.ExtensionNumber = ExtensionNumber.Text
            .identificatorAgent = text1.Text
            .identificatorClient = text3.Text
            .AgreeTerms = AcceptConditions.Checked





        End With




        _loading = False
    End Sub

#End Region








#Region "AutoPostBack Events Handles"

Protected Sub btnRegister_Click(sender As Object, e As EventArgs)  
        Dim UserRegistrationParameterInstance As UserRegistrationParameter = Nothing
        Dim _formContext As New InMotionGIT.Common.Contracts.Context(LanguageHelper.CurrentCultureToLanguage, _formData.InternalId)
        Dim currentAction As String = SessionTrace("btnRegister_Click")
        Dim parametersDictionary As Dictionary(Of String, Object) = Nothing  
        
        popupNotifyMessage.ShowOnPageLoad = false
        
        Try 
            Dim messageAction As String = String.Empty             
            Dim lastURL As String = String.Empty
            Dim isNullResult As Boolean = True
            Dim isFoundData As Boolean  = False             
            Dim existUserNameInternal As System.Boolean
Dim existEmailInternal As System.Boolean


        
           
        
           GetCurrentParameterInstance(UserRegistrationParameterInstance, True)
         
           ParametersToDictionary(parametersDictionary)  
        
           With parametersDictionary
    .Add("existUserNameInternal", existUserNameInternal)
    .Add("existEmailInternal", existEmailInternal)
End With
            currentAction = SessionTrace("1. If _formData.typeUser = 1 Then") 
        If _formData.typeUser = 1 Then 

            currentAction = SessionTrace("2. _formData.UserInformation.UserType = _formData.UserInformation.UserType....") 
 
If IsNothing(_formData.UserInformation) Then
   _formData.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
End If 
If IsNothing(_formData.UserInformation.UserType) Then
   _formData.UserInformation.UserType = New InMotionGIT.FrontOffice.Proxy.UserService.EnumPortaUserType
End If 

 _formData.UserInformation.UserType = _formData.UserInformation.UserType.Client 

        Else 
            currentAction = SessionTrace("3. _formData.UserInformation.UserType = _formData.UserInformation.UserType....") 
 
If IsNothing(_formData.UserInformation) Then
   _formData.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
End If 
If IsNothing(_formData.UserInformation.UserType) Then
   _formData.UserInformation.UserType = New InMotionGIT.FrontOffice.Proxy.UserService.EnumPortaUserType
End If 

 _formData.UserInformation.UserType = _formData.UserInformation.UserType.Agent 

            End If
            currentAction = SessionTrace("4. Call Function 'Exist' with parameters (userName)") 
            Dim _existUserNameInternal_ceea9b2dd7cf4158951f12d42190e9b5 As System.Boolean 
            _existUserNameInternal_ceea9b2dd7cf4158951f12d42190e9b5 = (New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient).Exist            (userName:=_formData.UserInformation.UserName) 
             
            If Not IsNothing(_existUserNameInternal_ceea9b2dd7cf4158951f12d42190e9b5) Then 
               existUserNameInternal = _existUserNameInternal_ceea9b2dd7cf4158951f12d42190e9b5 
            End If 
            isNullResult = (IsNothing(_existUserNameInternal_ceea9b2dd7cf4158951f12d42190e9b5)) 

            currentAction = SessionTrace("5. Call Function 'ExistEmail' with parameters (email)") 
            Dim _existEmailInternal_01c0e42e5a8a49349dc922f743020ccd As System.Boolean 
            _existEmailInternal_01c0e42e5a8a49349dc922f743020ccd = (New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient).ExistEmail            (email:=_formData.UserInformation.Email) 
             
            If Not IsNothing(_existEmailInternal_01c0e42e5a8a49349dc922f743020ccd) Then 
               existEmailInternal = _existEmailInternal_01c0e42e5a8a49349dc922f743020ccd 
            End If 
            isNullResult = (IsNothing(_existEmailInternal_01c0e42e5a8a49349dc922f743020ccd)) 

            currentAction = SessionTrace("6. If {existEmailInternal} = False AND {existUserNameInternal} = False Then") 
        If existEmailInternal = False AndAlso existUserNameInternal = False Then 

            currentAction = SessionTrace("7. If _formData.typeUser = 1 Then") 
        If _formData.typeUser = 1 Then 

            currentAction = SessionTrace("8. If _formData.identificatorClient Is Null Or Empty '' Then") 
        If String.IsNullOrEmpty(_formData.identificatorClient) Then 

            currentAction = SessionTrace("9. lblResult.Value = El código del cliente no puede estar vació.") 
lblResult.Value = "El código del cliente no puede estar vació."

            currentAction = SessionTrace("10. btnRegister.Enabled = False, lblResult.Visible = True, lblResult.Value = Se creo correctamente el usuario.") 
BehaviorShowControls("btnRegister,Disabled")
BehaviorShowControls("lblResult,Visible")
lblResult.Value = "Se creo correctamente el usuario."

        Else 
            currentAction = SessionTrace("11. _formData.UserInformation.ClientID = _formData.identificatorClient...") 
 
If IsNothing(_formData.UserInformation) Then
   _formData.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
End If 

 _formData.UserInformation.ClientID = _formData.identificatorClient 

            currentAction = SessionTrace("12. Call Shared Function 'SetPagesDefault' with parameters (user)") 
            Dim _UserInformation_8d5c7518e9384592af72b6bcf733cdb8 As InMotionGIT.FrontOffice.Proxy.UserService.UserInformation 
            _UserInformation_8d5c7518e9384592af72b6bcf733cdb8 = InMotionGIT.FrontOffice.Proxy.Helpers.Page.SetPagesDefault            (user:=_formData.UserInformation) 
             
            If Not IsNothing(_UserInformation_8d5c7518e9384592af72b6bcf733cdb8) Then 
               _formData.UserInformation = _UserInformation_8d5c7518e9384592af72b6bcf733cdb8 
            End If 
            isNullResult = (IsNothing(_UserInformation_8d5c7518e9384592af72b6bcf733cdb8)) 

            currentAction = SessionTrace("13. Call Sub 'CreatePortalUser' with parameters (user)") 
With (New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient)
    .CreatePortalUser            (user:=_formData.UserInformation) 
             
End With

            End If
        Else 
            currentAction = SessionTrace("14. If _formData.identificatorAgent Is Null Or Empty '' Then") 
        If String.IsNullOrEmpty(_formData.identificatorAgent) Then 

            currentAction = SessionTrace("15. lblResult.Value = El código del agente no puede estar vació.") 
lblResult.Value = "El código del agente no puede estar vació."

        Else 
            currentAction = SessionTrace("16. _formData.UserInformation.ProducerID = _formData.identificatorAgent...") 
 
If IsNothing(_formData.UserInformation) Then
   _formData.UserInformation = New InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
End If 

 _formData.UserInformation.ProducerID = _formData.identificatorAgent 

            currentAction = SessionTrace("17. Call Shared Function 'SetPagesDefault' with parameters (user)") 
            Dim _UserInformation_541985591a4444359fcebffd364cee04 As InMotionGIT.FrontOffice.Proxy.UserService.UserInformation 
            _UserInformation_541985591a4444359fcebffd364cee04 = InMotionGIT.FrontOffice.Proxy.Helpers.Page.SetPagesDefault            (user:=_formData.UserInformation) 
             
            If Not IsNothing(_UserInformation_541985591a4444359fcebffd364cee04) Then 
               _formData.UserInformation = _UserInformation_541985591a4444359fcebffd364cee04 
            End If 
            isNullResult = (IsNothing(_UserInformation_541985591a4444359fcebffd364cee04)) 

            currentAction = SessionTrace("18. Call Sub 'CreatePortalUser' with parameters (user)") 
With (New InMotionGIT.FrontOffice.Proxy.UserService.UsersClient)
    .CreatePortalUser            (user:=_formData.UserInformation) 
             
End With

            End If
            End If
        Else 
            currentAction = SessionTrace("19. lblResult.Visible = true") 
BehaviorShowControls("lblResult,Visible")

            currentAction = SessionTrace("20. lblResult.Value = Por favor verifique el email o nombre de usuario ya se encuentran en nuestro sistema") 
lblResult.Value = "Por favor verifique el email o nombre de usuario ya se encuentran en nuestro sistema"

            currentAction = SessionTrace("21. _formData.AgreeTerms = false...") 
 

 _formData.AgreeTerms = false 

            End If


          
           currentAction = String.Empty
             
        
           
                         
          
          SetCurrentParameterInstance(UserRegistrationParameterInstance,"")
          
        
          If UserRegistrationParameterInstance.Behavior <> InMotionGIT.FrontOffice.Support.Enumerations.enumBehavior.None Then
             If Not String.IsNullOrEmpty(UserRegistrationParameterInstance.NotifyMessage) Then
                 ShowWindowPopupMessage(UserRegistrationParameterInstance.NotifyMessage)
             End If           
            
             UserRegistrationParameterInstance.Behavior = InMotionGIT.FrontOffice.Support.Enumerations.enumBehavior.None
          End If
        
          If Not String.IsNullOrEmpty(UserRegistrationParameterInstance.BehaviorShowControls) Then
               BehaviorShowControls(UserRegistrationParameterInstance.BehaviorShowControls)
          End If        
                  
          If Not IsNothing(_formContext.Errors) AndAlso _formContext.Errors.Count > 0 Then
				_formData.Errors.AddErrorList(_formContext.Errors)
           End If         
          
      Catch ex As Exception
           InMotionGIT.Common.Helpers.LogHandler.ErrorLog(Page.AppRelativeVirtualPath, currentAction, ex)
		   _formData.Errors.Add(New InMotionGIT.Common.Contracts.Errors.Error With {.Message = currentAction & " " & ex.Message, .Severity = InMotionGIT.Common.Contracts.Errors.Enumerations.EnumSeverity.Error})

           
           If Request.QueryString("debug").IsNotEmpty Then
               FormMessageLabel.Text = InMotionGIT.Common.Helpers.ExceptionHandlers.TraceInnerExceptionMessage(ex, True)
               FormMessageLabel.ForeColor = Drawing.Color.Black
               FormMessageLabel.Font.Bold = True
               MessageTable.Visible = True
           End If           
           
      Finally
            If _formData.Errors.Count > 0 Then
               SetErrors(_formData.Errors)
            End If
            
             ClosePopupWindow()      
                
           If  _formData.Errors.Count = 0 AndAlso Not String.IsNullOrEmpty(_webTransfer) Then
               If Page.IsCallback Then
                  DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_webTransfer)
               Else
                   Response.ClearHeaders()        
                   Response.ClearContent()        
                   Response.Redirect(_webTransfer)
               End If          
           End If
		   UserRegistrationUpdatePanel.Update
      End Try         
    End Sub


#End Region

#Region "UserControls Events Handles"

    



#End Region

#Region "Form Manager"

    Private Sub VerifySecurityLevel()


    End Sub

      

    Private Sub GetCurrentParameterInstance(ByRef UserRegistrationParameterInstance As UserRegistrationParameter, force As Boolean)
        If _loadcompleted Or force Then
            If Not _CurrentParameterInstance Then

                UserRegistrationParameterInstance = FormData

                If Page.IsPostBack Then
                    MapPageToClass(UserRegistrationParameterInstance)
                End If

                _CurrentParameterInstance = True
            Else
                UserRegistrationParameterInstance = _formData
            End If
        Else
            UserRegistrationParameterInstance = Nothing
        End If
    End Sub

    Protected Sub SetCurrentParameterInstance(ByRef UserRegistrationParameterInstance As UserRegistrationParameter, calledBy As String)
        FormData = UserRegistrationParameterInstance
        MapClassToPage(UserRegistrationParameterInstance,calledBy)
    End Sub

    Protected Sub SetErrors(errors As InMotionGIT.Common.Contracts.Errors.ErrorCollection)
        Dim errorList As InMotionGIT.Common.Contracts.Errors.ErrorCollection = ViewState("Errors")

        If IsNothing(errorList) Then
            errorList = New InMotionGIT.Common.Contracts.Errors.ErrorCollection
        End If

        errorList = errors

        ViewState("Errors") = errorList
        ShowErrors()
    End Sub

    Public Sub ShowErrors()
        If Not IsNothing(ViewState("Errors")) Then
            Dim errorList As InMotionGIT.Common.Contracts.Errors.ErrorCollection = ViewState("Errors")

            Dim queryOut = From lst In errorList Select lst Order By lst.ErrorId

            ErrorsGridView.Visible = True
            ErrorsGridView.DataSource = queryOut.ToList
            ErrorsGridView.DataBind()
            UpdatePanelErrors.Update()
        End If
    End Sub
   
    Private Sub ShowWindowPopupMessage(message As String)
        NotifyMessageLabel.Text = message
        popupNotifyMessage.ShowOnPageLoad = True
    End Sub

    Private Sub ClosePopupWindow()
        popControl.Windows(0).ShowOnPageLoad = False
    End Sub

    Private Sub GetTransferParameters()
        Dim fromDocumentCache As Object = Nothing

        If Not String.IsNullOrEmpty(Request.QueryString("fromid")) Then
            fromDocumentCache = Session( Request.QueryString("fromid"))
            Session.Remove(Request.QueryString("fromid"))
            
        Else
            fromDocumentCache = Session(Session("fromid"))
            Session.Remove(Session("fromid"))
            Session.Remove("fromid")   
        End If
        
        If Not IsNothing(fromDocumentCache) Then
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("UserInformation", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("EmailVerification", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("AgreeTerms", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("TemporalUserName", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("identificatorAgent", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("typeUser", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("identificatorClient", fromDocumentCache, _formData)
            InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValue("Parameter8", fromDocumentCache, _formData)
            
        End If
    End Sub
    
    

    Private Sub SetDefaultValuesFromQueryString()

        If Request.Form("EmailVerification").IsNotEmpty Then
                FormData.EmailVerification = Request.Form("EmailVerification") 

ElseIf Request.QueryString("EmailVerification").IsNotEmpty Then
                FormData.EmailVerification = Request.QueryString("EmailVerification") 
End If 

If Request.Form("AgreeTerms").IsNotEmpty Then
                FormData.AgreeTerms = Request.Form("AgreeTerms") 

ElseIf Request.QueryString("AgreeTerms").IsNotEmpty Then
                FormData.AgreeTerms = Request.QueryString("AgreeTerms") 
End If 

If Request.Form("TemporalUserName").IsNotEmpty Then
                FormData.TemporalUserName = Request.Form("TemporalUserName") 

ElseIf Request.QueryString("TemporalUserName").IsNotEmpty Then
                FormData.TemporalUserName = Request.QueryString("TemporalUserName") 
End If 

If Request.Form("identificatorAgent").IsNotEmpty Then
                FormData.identificatorAgent = Request.Form("identificatorAgent") 

ElseIf Request.QueryString("identificatorAgent").IsNotEmpty Then
                FormData.identificatorAgent = Request.QueryString("identificatorAgent") 
End If 

If Request.Form("typeUser").IsNotEmpty Then
                FormData.typeUser = Request.Form("typeUser") 

ElseIf Request.QueryString("typeUser").IsNotEmpty Then
                FormData.typeUser = Request.QueryString("typeUser") 
End If 

If Request.Form("identificatorClient").IsNotEmpty Then
                FormData.identificatorClient = Request.Form("identificatorClient") 

ElseIf Request.QueryString("identificatorClient").IsNotEmpty Then
                FormData.identificatorClient = Request.QueryString("identificatorClient") 
End If 

If Request.Form("Parameter8").IsNotEmpty Then
                FormData.Parameter8 = Request.Form("Parameter8") 

ElseIf Request.QueryString("Parameter8").IsNotEmpty Then
                FormData.Parameter8 = Request.QueryString("Parameter8") 
End If 


    End Sub
    
    Private Sub BehaviorControls(controlItem As Control, isEnable As Boolean)
        InMotionGIT.FrontOffice.Support.Helpers.ReflectionHandler.AssignPropertyValueSimple("Enabled", controlItem, isEnable)           
    End Sub
    
    Private Sub SetReadOnlyControls()
    
        BehaviorControls(UserName, False)
        BehaviorControls(Email, False)
        BehaviorControls(EmailVerification, False)
        BehaviorControls(SecurityQuestion, False)
        BehaviorControls(SecurityAnswer, False)
        BehaviorControls(FirstName, False)
        BehaviorControls(SurName, False)
        BehaviorControls(LastName, False)
        BehaviorControls(SecondLastName, False)
        BehaviorControls(DateOfBirth, False)
        BehaviorControls(Gender, False)
        BehaviorControls(AddressHome, False)
        BehaviorControls(City, False)
        BehaviorControls(State, False)
        BehaviorControls(Country, False)
        BehaviorControls(AreaNumber, False)
        BehaviorControls(TelephoneNumber, False)
        BehaviorControls(ExtensionNumber, False)
        BehaviorControls(text1, False)
        BehaviorControls(text3, False)
        BehaviorControls(AcceptConditions, False)
        BehaviorControls(btnSeeTerms, False)
        BehaviorControls(btnRegister, False)
    
    End Sub
     
#End Region

#Region "Actions Data Methods"

    Private Function SessionTrace(message As String) As String

        If Not IsNothing(Session("Form.Track")) AndAlso Session("Form.Track").ToString.ToLower = "true" Then
            Dim tracelog As String = Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath)))

            tracelog += String.Format(CultureInfo.InvariantCulture, "{0} {1}<br>{2}", Now.ToString("hh:mm:ss.fff"), message, vbCrLf)
            Session(String.Format(CultureInfo.InvariantCulture, "Form.{0}.trace", IO.Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath))) = tracelog
        End If

        Return message
    End Function
    
    Private Sub ControlsInitialization()

    End Sub     
  
    Private Sub ParametersToDictionary(ByRef target As Dictionary(Of String, Object))
        If IsNothing(target) Then
            target = New Dictionary(Of String, Object)

            target.Add("UserInformation", _formData.UserInformation) 
            target.Add("EmailVerification", _formData.EmailVerification) 
            target.Add("AgreeTerms", _formData.AgreeTerms) 
            target.Add("TemporalUserName", _formData.TemporalUserName) 
            target.Add("identificatorAgent", _formData.identificatorAgent) 
            target.Add("typeUser", _formData.typeUser) 
            target.Add("identificatorClient", _formData.identificatorClient) 
            target.Add("Parameter8", _formData.Parameter8) 

        Else
            target("UserInformation") = _formData.UserInformation 
            target("EmailVerification") = _formData.EmailVerification 
            target("AgreeTerms") = _formData.AgreeTerms 
            target("TemporalUserName") = _formData.TemporalUserName 
            target("identificatorAgent") = _formData.identificatorAgent 
            target("typeUser") = _formData.typeUser 
            target("identificatorClient") = _formData.identificatorClient 
            target("Parameter8") = _formData.Parameter8 

        End If     
    End Sub
    
    
    Private Sub ExecuteActionsInitializationForm()
                    Dim currentAction As String = SessionTrace("ActionsInitializationForm")
                    
                    Try                          
                        Dim _formContext As New InMotionGIT.Common.Contracts.Context(LanguageHelper.CurrentCultureToLanguage, _formData.InternalId)                          
                        Dim messageAction As String = String.Empty
                        Dim lastURL As String = String.Empty
                        Dim isNullResult As Boolean = True 
                        Dim isFoundData As Boolean  = False 
                        Dim parametersDictionary As Dictionary(Of String, Object) = Nothing
                        
                        ParametersToDictionary(parametersDictionary)
                         
                        
                                    currentAction = SessionTrace("1. lblResult.Visible = False") 
BehaviorShowControls("lblResult,Hidden")

            currentAction = SessionTrace("2. If _formData.typeUser = 1 Then") 
        If _formData.typeUser = 1 Then 

            currentAction = SessionTrace("3. zoneIntermediary.Visible = False") 
BehaviorShowControls("zoneIntermediary,Hidden")

            currentAction = SessionTrace("4. zoneClient.Visible = True") 
BehaviorShowControls("zoneClient,Visible")

        Else 
            currentAction = SessionTrace("5. zoneClient.Visible = False") 
BehaviorShowControls("zoneClient,Hidden")

            currentAction = SessionTrace("6. zoneIntermediary.Visible = True") 
BehaviorShowControls("zoneIntermediary,Visible")

            End If

                        
                        currentAction = String.Empty
                         
                    Catch ex As Exception
                        InMotionGIT.Common.Helpers.LogHandler.ErrorLog(Page.AppRelativeVirtualPath, currentAction, ex)
						_formData.Errors.Add(New InMotionGIT.Common.Contracts.Errors.Error With {.Message = currentAction & " " & ex.Message, .Severity = InMotionGIT.Common.Contracts.Errors.Enumerations.EnumSeverity.Error})

                        If Request.QueryString("debug").IsNotEmpty Then
                            FormMessageLabel.Text = InMotionGIT.Common.Helpers.ExceptionHandlers.TraceInnerExceptionMessage(ex, True)
                            FormMessageLabel.ForeColor = Drawing.Color.Black
                            FormMessageLabel.Font.Bold = True
                            MessageTable.Visible = True
                        End If
                        
                    Finally                     
                        If _formData.Errors.Count > 0 Then
                            SetErrors(_formData.Errors)
                        End If
                        
                        If _formData.Errors.Count = 0 AndAlso Not String.IsNullOrEmpty(_webTransfer) Then
                            If Page.IsCallback Then
                                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(_webTransfer)
                            Else
                                Response.ClearHeaders()
                                Response.ClearContent()
                                Response.Redirect(_webTransfer)
                            End If
                        End If
                   End Try
               End Sub
    
    
#End Region






End Class

<Serializable()>
Public Class UserRegistrationParameter
    Inherits InMotionGIT.FrontOffice.Support.DataType.FormBase(Of  UserRegistrationParameter)

    ' Methods
    Public Sub New()
        MyBase.New()
    End Sub

    ' Properties
    Public Property UserInformation As InMotionGIT.FrontOffice.Proxy.UserService.UserInformation
    Public Property EmailVerification As String
    Public Property AgreeTerms As Boolean
    Public Property TemporalUserName As String
    Public Property identificatorAgent As String
    Public Property typeUser As Int32
    Public Property identificatorClient As String
    Public Property Parameter8 As String


End Class