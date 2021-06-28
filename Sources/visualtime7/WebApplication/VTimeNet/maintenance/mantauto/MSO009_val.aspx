<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eRemoteDB.Parameter" %>

<script language="VB" runat="Server">
    Private mstrErrors As String
    Private mobjValues As eFunctions.Values
    Private mstrString As String

'+ Se define la contante para el manejo de errores en caso de advertencias
    Private mstrCommand As String

    ''' <summary>
    ''' Se realizan las validaciones masivas de la forma
    ''' </summary>
    Function insValidation() As String
        Dim result As String = String.Empty
        With Request
            Select Case .QueryString.Item("sCodispl")
                Case "MSO009"                    
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Dim lclsErrors As New eFunctions.Errors

                                Dim maxEffecdate As System.DateTime
                                Dim rdb As New eRemoteDB.Execute

                            If String.IsNullOrEmpty(.Form.Item("cbeBranch")) OrElse .Form.Item("cbeBranch") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000044)                          
                                
                                End If
                            If String.IsNullOrEmpty(.Form.Item("valProduct")) OrElse .Form.Item("valProduct") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 11009)                          
                                
                                End If
                            If String.IsNullOrEmpty(.Form.Item("NCURRENCY")) OrElse .Form.Item("NCURRENCY") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 10107)                          
                                                                
                                End If
                  rdb = New eRemoteDB.Execute 
           rdb.SQL = "SELECT  MAX(TAR_SOAP.DEFFECDATE) AS ROWMAX FROM INSUDB.TAR_SOAP TAR_SOAP  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NCURRENCY = :NCURRENCY"
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.Form.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

       
            If rdb.Run(True) Then
               Do While Not rdb.EOF
            maxEffecdate = rdb.FieldToClass("ROWMAX")  
                 
                 rdb.RNext()
               Loop                 
               rdb.RCloseRec()
            End If
                                If Request.QueryString.Item("nMainAction") <> 401 AND mobjValues.StringToType(.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) < maxEffecdate Then 
            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 10869) 
End If 

                            If String.IsNullOrEmpty(.Form.Item("DEFFECDATE")) Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 7114)                          
                                
                                End If
		
                            result = lclsErrors.Confirm
                        Else
                            If .QueryString.Item("WindowType") = "PopUp" Then
                                Dim lclsErrors As New eFunctions.Errors
                                                              
                                 Dim rowCountKey As System.Int32
                                Dim rdb As New eRemoteDB.Execute

                            If String.IsNullOrEmpty(.Form.Item("NMODULEC")) OrElse .Form.Item("NMODULEC") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 11117)                          
                                                                
                                End If
                            If String.IsNullOrEmpty(.Form.Item("NCOVER")) OrElse .Form.Item("NCOVER") = "0" Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 60321)
                                                                
                                End If
                            If String.IsNullOrEmpty(.Form.Item("NVEHTYPE")) OrElse .Form.Item("NVEHTYPE") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 13988)                          
                                                                
                                End If
                            If String.IsNullOrEmpty(.Form.Item("NPREMIUM")) OrElse .Form.Item("NPREMIUM") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 55614)                          
                                
                                End If
                  rdb = New eRemoteDB.Execute 
                            rdb.SQL = "SELECT  COUNT(TAR_SOAP.NBRANCH) AS ROWCOUNT FROM INSUDB.TAR_SOAP TAR_SOAP  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NCURRENCY = :NCURRENCY AND NVEHTYPE = :NVEHTYPE AND NAGREEMENT = :NAGREEMENT AND DEFFECDATE <= :DEFFECDATE AND (DNULLDATE IS NULL OR DNULLDATE > :DNULLDATE0)"
                            rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.Form.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.Form.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("valAgreement")), 0, .Form.Item("valAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("DNULLDATE0", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

       
            If rdb.Run(True) Then
               Do While Not rdb.EOF
            rowCountKey = rdb.FieldToClass("ROWCOUNT")  
                 
                 rdb.RNext()
               Loop                 
               rdb.RCloseRec()
            End If
                                If Request.QueryString.Item("Action") = "Add" AND rowCountKey > 0 Then 
            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000076) 
                End If 
             
                                	
                                result = lclsErrors.Confirm
                            End If
                        End If
                                       
                Case Else
                    result = "insValMSO009: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
            End Select
        End With
        Return result
    End Function
   
   

   
  
    ''' <summary>
    ''' Se realizan las actualizaciones a las tablas
    ''' </summary>
    Function insPosting() As Boolean
        Dim lblnPost As Boolean = False
        Dim rdb As eRemoteDB.Execute
        
        With Request
             Select Case Request.QueryString.Item("sCodispl")
                  Case "MSO009" 
		  
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                           mstrString = "&cbeBranch=" & .Form.Item("cbeBranch")  &"&valProduct=" & .Form.Item("valProduct")  &"&NCURRENCY=" & .Form.Item("NCURRENCY")  &"&DEFFECDATE=" & .Form.Item("DEFFECDATE")                         
                           lblnPost = True	
                        			
                        Else
							'CleanRatingCache()
                           If .QueryString.Item("WindowType") = "PopUp" Then
                               mstrString = "&cbeBranch=" & .QueryString.Item("cbeBranch")  &"&valProduct=" & .QueryString.Item("valProduct")  &"&NCURRENCY=" & .QueryString.Item("NCURRENCY")  &"&DEFFECDATE=" & .QueryString.Item("DEFFECDATE")    
                        
                               Select Case .QueryString.Item("Action")
                                   Case "Add" 
 rdb = New eRemoteDB.Execute 
                                    rdb.SQL = "INSERT INTO INSUDB.TAR_SOAP (NMODULEC, NCOVER, NVEHTYPE, NPREMIUM, NUSERCODE, DCOMPDATE, NBRANCH, NPRODUCT, NCURRENCY, DEFFECDATE, NAGREEMENT) VALUES (:NMODULEC, :NCOVER, :NVEHTYPE, :NPREMIUM, :NUSERCODE, SYSDATE, :NBRANCH, :NPRODUCT, :NCURRENCY, :DEFFECDATE, :NAGREEMENT)"
    
 rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.Form.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.Form.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMIUM", mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("valAgreement")), 0, .Form.Item("valAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
                                
                                   Case "Update" 
         If mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = mobjValues.StringToType(.Form.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate) Then 

 rdb = New eRemoteDB.Execute 
                                        rdb.SQL = "UPDATE INSUDB.TAR_SOAP SET NPREMIUM = :NPREMIUM, NUSERCODE = :NUSERCODE, DCOMPDATE = SYSDATE , NBRANCH = :NBRANCH , NPRODUCT = :NPRODUCT , NCURRENCY = :NCURRENCY, NAGREEMENT = :NAGREEMENT WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NCURRENCY = :NCURRENCY AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NVEHTYPE = :NVEHTYPE AND DEFFECDATE = :DEFFECDATE AND NAGREEMENT = :NAGREEMENT "
    
 rdb.Parameters.Add("NPREMIUM", mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.Form.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.Form.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("valAgreement")), 0, .Form.Item("valAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
         Else 

 rdb = New eRemoteDB.Execute 
                                        rdb.SQL = "UPDATE INSUDB.TAR_SOAP SET DNULLDATE = :DNULLDATE, NUSERCODE = :NUSERCODE, DCOMPDATE = SYSDATE WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NCURRENCY = :NCURRENCY AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NVEHTYPE = :NVEHTYPE AND DEFFECDATE = :DEFFECDATE AND NAGREEMENT = :NAGREEMENT "
    
 rdb.Parameters.Add("DNULLDATE", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.Form.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.Form.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.Form.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("valAgreement")), 0, .Form.Item("valAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
rdb = New eRemoteDB.Execute 
          rdb.SQL = "INSERT INTO INSUDB.TAR_SOAP (NMODULEC, NCOVER, NVEHTYPE, NPREMIUM, NUSERCODE, DCOMPDATE, NBRANCH, NPRODUCT, NCURRENCY, DEFFECDATE, NAGREEMENT) VALUES (:NMODULEC, :NCOVER, :NVEHTYPE, :NPREMIUM, :NUSERCODE, SYSDATE, :NBRANCH, :NPRODUCT, :NCURRENCY, :DEFFECDATE, :NAGREEMENT)"
    
 rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(.Form.Item("NMODULEC"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.Form.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMIUM", mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(.QueryString.Item("NCURRENCY"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("NAGREEMENT", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("valAgreement")), 0, .Form.Item("valAgreement")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
        End if
                           
                              End Select
                           Else
                              lblnPost = True
                           End If
                       End If             		
             End Select
        
            
	      End With
               
        Return lblnPost
    End Function

        ''' <summary>
        ''' Se encarga de limpiar el cache utilizado por el rating engine del frontoffice
        ''' </summary>
        Private Sub CleanRatingCache()
            Dim url As String = ConfigurationManager.AppSettings("Url.FrontOffice")
            If String.IsNullOrEmpty(url) Then
                url = "http://200.27.121.45"
            End If
            url = String.Format("{0}/support/cache.ashx?clean=y", url)
            Try
                Dim localRequest As Net.WebRequest = Net.WebRequest.Create(url)
                localRequest.Credentials = Net.CredentialCache.DefaultCredentials
                Dim response As Net.WebResponse = localRequest.GetResponse()
                Dim dataStream As IO.Stream = response.GetResponseStream()
                Dim reader As New IO.StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
            Catch ex As Exception
                InMotionGIT.Common.Helpers.ExceptionHandlers.ErrorLog("MSO009 FullCleanCache", ex.Message, 0)
            End Try
        End Sub
		
</script>
<%
    Response.Expires = -1441
    mobjValues = New eFunctions.Values

    mobjValues.sCodisplPage = "MSO009val"
    mstrCommand = "sModule=Maintenance&sProject=MantAuto&sCodisplReload=" & Request.QueryString.Item("sCodispl") & "&sValPage=" & "MSO009_val"
%>
<html>
<head>
    <title></title>
    <%
        With Response
					.Write(mobjValues.StyleSheet())
					.Write(mobjValues.WindowsTitle("GE002", Request.QueryString.Item("sWindowDescript")))
				End With
		%>
    <script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript">
    
				//------------------------------------------------------------------------------------------
				function NewLocation(Source,Codisp){
				//------------------------------------------------------------------------------------------
				    var lstrLocation = "";
				    lstrLocation += Source.location;
				    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
				    Source.location = lstrLocation
				}
    </script>
</head>
<body>
    <form id="form1" action="">
    <%
         
		    '+ Si no se han validado los campos de la página
		    If Request.Form.Item("sCodisplReload") = vbNullString Then
		        mstrErrors = insValidation()
		        Session("sErrorTable") = mstrErrors
		        Session("sForm") = Request.Form.ToString
		    Else
		        Session("sErrorTable") = vbNullString
		        Session("sForm") = vbNullString
		    End If
		
		    If mstrErrors > vbNullString Then
		        With Response
		            .Write("<script type='text/javascript' language='JavaScript'>")
		            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""MantPolicyError"",660,330);")
		            .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		            .Write("</script>")
		        End With
		    Else
		        If insPosting() Then
		            If Request.QueryString.Item("WindowType") <> "PopUp" Then
		                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
		                    If Request.Form.Item("sCodisplReload") = vbNullString Then
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.document.location.reload();</script>")
		                    Else
		                        Response.Write("<script type='text/javascript' language='JavaScript'>window.close();opener.insReloadTop(true,false);</script>")
		                    End If
		                Else
		                    If Request.QueryString.Item("nZone") = "1" Then
		                        If Request.Form.Item("sCodisplReload") = vbNullString Then
		                            If Request.QueryString.Item("sCodispl") = "MSO009" Then
		                                Response.Write("<script type='text/javascript' language='JavaScript'>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & mstrString & """;</script>")
		                            Else
		                                Response.Write("<script type='text/javascript' language='JavaScript'>insReloadTop();</script>")
		                            End If

		                        Else
		                            Response.Write("<script type='text/javascript' language='JavaScript'>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & mstrString & """;</script>")
		                        End If

		                    Else
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & """;</script>")
		                    End If
		                End If
		            Else
					
		                '+ Se recarga la página que invocó la PopUp					
		                Select Case Request.QueryString.Item("sCodispl")
		                    Case "MSO009"
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.opener.document.location.href='MSO009.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "' </script>")
		                End Select
		            End If		            
		        End If
		    End If
    %>
    </form>
</body>
</html>