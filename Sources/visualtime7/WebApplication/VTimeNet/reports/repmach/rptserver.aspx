<%@ Page LANGUAGE="VB" %>
<script language="VB" runat="Server">
Dim EPFMIMETYPE As String
Dim goPageGeneratorDrillonMap As Object
Dim temp As Object
Dim val As Integer
Dim ETFMIMETYPE As String
Dim gvGroupNameDD As Object
Dim tempNumber As Integer
Dim tmpArray As Object


Dim goPageGenerator As Object ' page generator object
Dim goPageCollection() As Object ' page collection object
Dim goPageGeneratorDrill As Object ' page generator object in Drill Down Context
Dim goPage As Object ' the page object
Dim gvGroupPathDD As Object ' drill down group path, this is an array.
Dim gvGroupPath() As Short ' this is branch, aka Group Path converted from string passed on the QS, it is an Array
Dim gvGroupLevel() As Short ' this is the Group level, converted from the string passed on the QS, it is an Array
Dim gvMaxNode() As Object ' this represents the number of nodes to retrieve for the totaller, it is set to an empty array
Dim gvTotallerInfo() As Short ' this represents the group path of the requested totaller.
Dim glX As String ' this is the X Coordinate for a drill down on a graph or Map
Dim glY As String ' this is the Y Coordinate for a drill down on a graph or Map
Dim gvPageNumber As Integer ' holds the requested page number
Dim gvURL As String ' URL to redirect to
Dim gsErrorText As String ' holds the error text to be sent to the viewer.
Dim ExportOptions As Object ' Export Options Object 
Dim slX As String ' this is the X Coordinate for a selection of Out of Place subreport
Dim slY As String ' this is the Y Coordinate for a selection of Out of Place subreport

' Vaiables that represent what was passed on the Query String
Dim CMD As Object ' This determines the main function to perform
Dim PAGE As Object ' the page to return
Dim BRCH As Object ' the branch is a mechanism to determine the drill down level.
' A drill down level is like a view of the report, a new tab
' is created to indicate that it is a new view
Dim VIEWER As String ' This is the viewer that is calling the server
Dim VFMT As Object ' the format that the viewer understands
Dim NODE As String ' Currently not used??
Dim GRP As Object ' this is a way of specifing the actual group
Dim COORD As String ' these are the coordinates on the graph to process
'UPGRADE_NOTE: DIR was upgraded to DIR_Renamed. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1061.aspx'
Dim DIR_Renamed As String ' this is the search direction
Dim CSE As String ' indicates if the search is case sensitive
Dim TEXT As String ' this is the text to search for.
Dim INIT As String ' used to build the frames for the html viewer
Dim NEWBRCH As String ' used to keep track of when a new branch is to be viewed.
Dim EXPORT_FMT As String ' used to hold the export format and type
Dim SUBRPT As String ' used to hold the Out Of Place Subreport page, number,
' and coordinates on the main report.
Dim INCOMPLETE_PAGE As Short ' used to indicate whether the page generated should contain placeholders.
Dim INCOMPLETE_PAGE_COUNT As Short ' used to indicate whether the page should contain the total page count if not yet generated.
Dim PVERSION As Short ' used to indicate the protocol version of the viewer.
Dim TTL_INFO As Object ' used to indicate the group path of the totaller request.
' Constant Values 
Dim CREFTWORDFORWINDOWS As Byte
Dim CREFTRICHTEXT As Byte
Dim CREFTEXCEL21 As Byte
Dim CREFTCRYSTALREPORT As Byte
Dim CREDTDISKFILE As Byte
Dim EMFMIMETYPE As String
' for html browser send back the page
Dim appendQuery As String
' We are being called by HTML viewer
' need to get the text from the form post
Dim searchFound As Byte




Sub RetrieveObjects()
	' This procedure simply retrieves the session objects into global variables.
	' In the case of Out of Place Subreports, the SUBRPT parameter must be parsed and the
	' Subreport page generator object must be created.
	Dim oRptOptions As Object 'Report Options 
	Dim tmpCharIndexVal As Object
	Dim charIndexVal As Double
	Dim tmpStr As String
	Dim tmpPageGenerator As Object
	Dim subPageGenerator As Object
	Dim OOPSSeqNo As String 'holds the page's OOPS sequence number
	Dim OOPSSubName As String 'holds the OOPS's name
	Dim subCoords As String 'holds the coordinates of the OOPS in the main report
	Dim subgvGroupPath() As Short 'holds the group path for the main report in subrpt parameter
	Dim mainRptPageNumber As String 'holds the page number for the main report in the subrpt parameter
	
	'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	subgvGroupPath = New Short(){}
	If Not IsNothing(session("owPageEngine")) Then
		' make sure dialogs have been disabled
		If SUBRPT <> "" Then
			' Obtain the subreport sequence number
			charIndexVal = findChar(SUBRPT, ":")
			If charIndexVal > 1 Then
				OOPSSeqNo = Mid(SUBRPT, 1, charIndexVal - 1)
			End If
			' Obtain the subreport's name
			tmpStr = Mid(SUBRPT, charIndexVal + 1)
			charIndexVal = findChar(tmpStr, ":")
			If charIndexVal > 1 Then
				OOPSSubName = Mid(tmpStr, 1, charIndexVal - 1)
			End If
			tmpStr = Mid(tmpStr, charIndexVal + 1)
			charIndexVal = findChar(tmpStr, ":")
			' Obtain the group path for the Out of Place Subreport
			If charIndexVal > 1 Then
				subgvGroupPath = CreateArray(Mid(tmpStr, 1, charIndexVal - 1))
			End If
			'Obtain the main report page number after the fourth : character
			tmpStr = Mid(tmpStr, charIndexVal + 1)
			'Get the location of the fourth : seperator
			charIndexVal = findChar(tmpStr, ":")
			mainRptPageNumber = Mid(tmpStr, 1, charIndexVal - 1)
			'Get the coordinates portion of the SUBRPT parameter
			subCoords = Mid(tmpStr, charIndexVal + 1)
			Call GetDrillDownCoordinates(subCoords, slX, slY)
			' Get the main reports page generator for the view
			tmpPageGenerator = session("owPageEngine").CreatePageGenerator(subgvGroupPath)
			subPageGenerator = tmpPageGenerator.DrillOnSubreport(mainRptPageNumber, slX, slY)
			goPageGenerator = subPageGenerator.CreateSubreportPageGenerator(gvGroupPath)
		Else
			goPageGenerator = session("owPageEngine").CreatePageGenerator(gvGroupPath)
		End If
		goPageCollection = goPageGenerator.Pages
	Else
		' must have timed out return an error, you may wan to Append to the
		' IIS log here.
		If VFMT = "ENCP" Then
			Response.ContentType = EMFMIMETYPE
			session("owEMF").SendErrorMsg(1, "User Session has expired")
		Else
			Response.Write("User Session has expired")
			
		End If
		Response.End()
	End If
	
End Sub

Sub ParseQS()
	Dim TAB_Renamed As String
	' Parse the Query String 
	CMD = UCase(request.querystring("cmd")) ' This determines the main function to perform
	PAGE = UCase(request.querystring("page")) ' the page to return
	BRCH = UCase(request.querystring("BRCH")) ' the branch is a mechanism to determine the drill down level.
	' A drill down level is like a view of the report, a new tab
	' is created to indicate that it is a new view
	VIEWER = UCase(request.querystring("VIEWER")) ' This is the viewer that is calling the server
	VFMT = UCase(request.querystring("VFMT")) ' the format that the viewer understands
	NODE = UCase(request.querystring("NODE"))
	GRP = UCase(request.querystring("GRP")) ' this is a way of specifing the actual group
	COORD = UCase(request.querystring("COORD")) ' these are the coordinates on the graph to process
	DIR_Renamed = UCase(request.querystring("DIR")) ' this is the search direction
	CSE = UCase(request.querystring("CASE")) ' indicates if the search is case sensitive
	TEXT = request.querystring("TEXT") ' this is the text to search for.
	INIT = UCase(request.querystring("INIT")) ' used to build the frames for the html viewer
	TAB_Renamed = UCase(request.querystring("TAB")) ' used to keep track of TABS on drill down.
	EXPORT_FMT = UCase(request.querystring("EXPORT_FMT")) ' Used to specify export format and type.	
	SUBRPT = UCase(request.querystring("SUBRPT")) ' The Out of Place Subreport coordinates.
	INCOMPLETE_PAGE = CShort(request.querystring("INCOMPLETE_PAGE")) ' Used to specify whether the page is to contain placeholders.
	INCOMPLETE_PAGE_COUNT = CShort(request.querystring("INCOMPLETE_PAGE_COUNT")) ' Used to specify whether the page has to contain a total page count.
	PVERSION = CShort(request.querystring("PVERSION")) ' Used to indicate the protocol version the viewer is utilizing.
	TTL_INFO = UCase(request.querystring("TTL_INFO")) 'Used to indicate the group path of the totaller request.
	
	' Initialize variables to a default if they are not provided on the query string.
	' Check for Parameter Values that are passed by the HTTP Post Command.
	If CMD = "" Then
		CMD = UCase(request.form("cmd"))
		If CMD = "" Then
			CMD = "GET_PG"
		End If
	End If
	
	If INIT = "" Then
		INIT = UCase(request.form("INIT"))
	End If
	
	If BRCH = "" Then
		BRCH = UCase(request.form("BRCH"))
	End If
	
	If BRCH = "" And INIT = "HTML_FRAME" Then
		Call InitializeFrameArray()
	End If
	
	
	If BRCH <> "" And INIT = "HTML_FRAME" Then
		If session("wlastBrch") <> BRCH Then
			NEWBRCH = "1"
		End If
	End If
	
	
	If VIEWER = "" Then
		VIEWER = UCase(request.form("VIEWER"))
		If VIEWER = "" Then
			VIEWER = "HTML"
		End If
	End If
	
	If VFMT = "" Then
		VFMT = UCase(request.form("VFMT"))
		If VFMT = "" Then
			VFMT = "HTML_PAGE"
		End If
	End If
	
	If GRP = "" Then
		GRP = UCase(request.form("GRP"))
	End If
	
	If TTL_INFO = "" Then
		TTL_INFO = UCase(request.form("TTL_INFO"))
	End If
	
	If COORD = "" Then
		COORD = UCase(request.form("COORD"))
	End If
	
	If NODE = "" Then
		NODE = UCase(request.form("NODE"))
	End If
	
	If DIR_Renamed = "" Then
		DIR_Renamed = UCase(request.form("DIR"))
		If DIR_Renamed = "" Then
			DIR_Renamed = "FOR" ' forward
		End If
	End If
	
	If CSE = "" Then
		CSE = UCase(request.form("CASE"))
		If CSE = "" Then
			CSE = "0" ' case insensitive
		End If
	End If
	
	If TEXT = "" Then
		TEXT = request.form("TEXT")
	End If
	
	If EXPORT_FMT = "" Then
		EXPORT_FMT = UCase(request.form("EXPORT_FMT"))
	End If
	
	If SUBRPT = "" Then
		SUBRPT = UCase(request.form("SUBRPT"))
	End If
	
	If request.form("INCOMPLETE_PAGE") <> "" Then
		INCOMPLETE_PAGE = CShort(request.form("INCOMPLETE_PAGE"))
	End If
	
	If request.form("INCOMPLETE_PAGE_COUNT") <> "" Then
		INCOMPLETE_PAGE_COUNT = CShort(request.form("INCOMPLETE_PAGE_COUNT"))
	End If
	
	If PVERSION = 0 Then
		PVERSION = CShort(request.form("PVERSION"))
	End If
	
	' Check to make sure there is a page requested, if not use 1 as a default
	If PAGE = "" Then
		PAGE = UCase(request.form("page"))
		If PAGE = "" Then
			PAGE = "1"
		End If
	End If
	
	If PAGE <> "" And Not IsNumeric(PAGE) Then
		PAGE = "1"
	End If
	
End Sub

Function CreateArray(ByVal vsStringArray As Object) As Short()
	Dim liStringLength As Double
	Dim x As Integer
	' this function takes an string like 0-1-1-0 and converts
	' it into an array of integers
	
	Dim lvArray() As Short
	Dim lvNewArray() As Short
	Dim liCount As Double
	Dim liCurrentPos As Double
	Dim lsBuf As String
	'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	lvArray = New Short(){}
	'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	lvNewArray = New Short(){}
	ReDim lvArray(256)
	
	liStringLength = Len(CStr(vsStringArray))
	liCount = 0
	liCurrentPos = 1
	lsBuf = ""
	
	While liCurrentPos <= liStringLength
		
		'ignore this character
		If Mid(vsStringArray, liCurrentPos, 1) <> "-" Then
			lsBuf = lsBuf & Mid(vsStringArray, liCurrentPos, 1)
			If liCurrentPos = liStringLength Then
				lvArray(liCount) = CShort(lsBuf)
				lsBuf = ""
				liCount = liCount + 1
			End If
			
		Else
			lvArray(liCount) = CShort(lsBuf)
			lsBuf = ""
			liCount = liCount + 1
		End If
		liCurrentPos = liCurrentPos + 1
	End While
	
	ReDim lvNewArray(liCount - 1)
	
	For x = 0 To (liCount - 1)
		lvNewArray(x) = lvArray(x)
	Next 
	
	
	CreateArray = lvNewArray.Clone()
	
End Function

' Helper function to parse coordinates passed by viewers and place into independent variables.
Sub GetDrillDownCoordinates(ByVal strParam As String, ByRef xCoord As String, ByRef yCoord As String)
	Dim x As Integer
	Dim liStringLength As Integer
	Dim lbDone As Boolean
	Dim lsBuf As String
	
	liStringLength = Len(strParam)
	lbDone = False
	lsBuf = ""
	xCoord = ""
	yCoord = ""
	For x = 1 To liStringLength
		lsBuf = Mid(strParam, x, 1)
		
		'ignore this character
		If lsBuf = "-" Then
			lsBuf = ""
			lbDone = True
		End If
		
		If lbDone Then
			yCoord = yCoord & lsBuf
		Else
			xCoord = xCoord & lsBuf
		End If
		
	Next 
	
End Sub

' This helper procedure will check if the requested page number exists.
' If it does not, it will set it to the last available page.
Sub ValidatePageNumber()
	If Err.Number <> 0 Then
		If Err.Number = 9 Then
			' just return the last page
			PAGE = goPageCollection.count
			goPage = goPageCollection(PAGE)
			' these session variables are used for the HTML Frame viewer
			session("wLastPageNumber") = PAGE
			session("wCurrentPageNumber") = PAGE
			Err.Clear()
		Else
			' A more serious error has occurred. Error message sent to browser.
			Call CheckForError()
		End If
	End If
End Sub



'  This helper procedure will send an error msg to the browser based on what viewer is being used.
Sub CheckForError()
	If Err.Number <> 0 Then
		If VFMT = "ENCP" Then
			Response.ContentType = EMFMIMETYPE
			session("owEMF").SendErrorMsg(1, "CRAXDRT Error Occured on Server. " & Err.Number & " : " & Err.Description)
		Else
			Response.Write("CRAXDRT Error Occured on Server. Error Number: " & Err.Number & " Error Description: " & Err.Description)
		End If
		Response.End()
	End If
End Sub

Sub InitializeFrameArray()
	'initialize the html_frame array
	'UPGRADE_NOTE: Object session() may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	session("wtabArray") = Nothing
	session("wlastBrch") = ""
	Dim tmpArray() As Object
	'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	tmpArray = New Object(){4}
	ReDim tmpArray(4)
	'Initialize the sequence number
	tmpArray(0) = "EMPTY"
	session("wtabArray") = tmpArray.Clone()
End Sub

' Helper function to parse the EXPORT_FMT parameter and fill in the properties of the 
' Export object.
Function FillExportOptionsObject(ByRef export_fmt_options As String) As Boolean
	Dim charIndex As Double
	Dim exportType As String
	Dim exportDLLName As Object
	charIndex = findChar(export_fmt_options, ":")
	If (charIndex > 0) Then
		'Get the export format type value
		exportType = Mid(export_fmt_options, charIndex + 1)
		exportDLLName = UCase(Mid(export_fmt_options, 1, charIndex - 1))
		Select Case exportDLLName
			Case "U2FWORDW"
				ExportOptions.FormatType = CREFTWORDFORWINDOWS + CShort(exportType)
				Response.ContentType = "application/msword"
			Case "U2FRTF"
				ExportOptions.FormatType = CREFTRICHTEXT + CShort(exportType)
				Response.ContentType = "application/rtf"
			Case "U2FXLS"
				ExportOptions.FormatType = CREFTEXCEL21 + CShort(exportType)
				Response.ContentType = "application/x-msexcel"
			Case "U2FCR"
				ExportOptions.FormatType = CREFTCRYSTALREPORT + CShort(exportType)
				Response.ContentType = "application/x-rpt"
			Case Else
				FillExportOptionsObject = False
				Exit Function
		End Select
		ExportOptions.DestinationType = CREDTDISKFILE
		FillExportOptionsObject = True
	Else
		FillExportOptionsObject = False
	End If
	
End Function

'  Helper function that returns the index of the character in the given string.
Function findChar(ByRef findStr As String, ByRef charToFind As String) As Double
	Dim charCounter As Double
	Dim tmpChar As String
	Dim lenStr As Double
	Dim result As Double
	lenStr = Len(findStr)
	result = -1
	If (lenStr > 0) Then
		charCounter = 1
		Do While (charCounter <= lenStr)
			tmpChar = Mid(findStr, charCounter, 1)
			If (tmpChar = charToFind) Then
				result = charCounter
				Exit Do
			End If
			charCounter = charCounter + 1
		Loop 
	End If
	
	findChar = result
End Function

</script>
<%
On Error Resume Next

If IsNothing(session("owEMF")) Then
'UPGRADE_NOTE: The 'CREmfgen.CREmfgen.1' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	session("owEMF") = Server.CreateObject("CREmfgen.CREmfgen.1")
	Call CheckForError()
End If
CREFTWORDFORWINDOWS = 14
CREFTRICHTEXT = 4
CREFTEXCEL21 = 18
CREFTCRYSTALREPORT = 1
CREDTDISKFILE = 1
'CRAXDRT.CRPlaceHolderType.crAllowPlaceHolders = 2
'CRAXDRT.CRPlaceHolderType.crDelayTotalPageCountCalc = 1
EMFMIMETYPE = "application/x-emf"
EPFMIMETYPE = "application/x-epf"
ETFMIMETYPE = "application/x-etf"
'	Initialize Arrays
'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
gvGroupPath = New Short(){}
'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
gvGroupLevel = New Short(){}
'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
gvMaxNode = New object(){} ' reteive all nodes
'UPGRADE_WARNING: Array has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
gvTotallerInfo = New Short(){}
NEWBRCH = "0"
'  To ensure that the browser does not cache the html pages for the group trees.
Response.Expires = 0
' Parse Query String for paramaters
Call ParseQS()

' INIT is a special QS case, we only care about HTML viewer, if it is then save send page and branch info
' to the frame page

If INIT = "HTML_FRAME" Then
	' build URL and send the QS
	If BRCH <> "" And NEWBRCH = "1" Then
		' htmstart is the base page that creates the frames for the HTML viewer
		' if there is branch information it needs to be passed along.
		tmpArray = session("wtabArray")
		If tmpArray(0) <> "EMPTY" Then
			val = UBound(tmpArray, 1) + 1
			ReDim Preserve tmpArray(val + 4)
		Else
			val = 0
		End If
		tmpArray(val) = CStr(val)
		tmpArray(val + 1) = session("wlastBrch")
		session("wlastBrch") = BRCH
		tmpArray(val + 2) = session("wCurrentPageNumber")
		tmpArray(val + 3) = session("wlastknownpage")
		tmpArray(val + 4) = session("wLastPageNumber")
		session("wtabArray") = tmpArray
		gvURL = "htmstart.aspx?brch=" & BRCH & "&"
	Else
		If BRCH <> "" Then
			gvURL = "htmstart.aspx?brch=" & BRCH
		Else
			gvURL = "htmstart.aspx"
		End If
	End If
	Response.redirect(gvURL)
End If



' If there is a BRCH then create the gvGroupPath array that represents it.

If BRCH <> "" Then
	gvGroupPath = CreateArray(BRCH)
End If

' If there is a GRP then create the gvGroupLevel array that represents it.

If GRP <> "" Then
	gvGroupLevel = CreateArray(GRP)
End If

' If there is a TTL_INFO then create the gvTotallerInfo array that represents it.

If TTL_INFO <> "" Then
	gvTotallerInfo = CreateArray(TTL_INFO)
End If



' If there are COORDs, then get them
If COORD <> "" Then
	Call GetDrillDownCoordinates(COORD, glX, glY)
End If

' This case statement determines what action to perform based on CMD
' there are sub cases for each viewer type



Select Case CMD
	
	Case "GET_PG"
		
		Call RetrieveObjects()
		
		' create the actual page
		goPage = goPageCollection(PAGE)
		' check for an exception on the page number 
		Call ValidatePageNumber()
		
		' 0 is for epf, 8209 is a SafeArray
		Select Case VFMT
			
			Case "ENCP"
				session("owPageEngine").PlaceHolderOptions = 0
				If (PVERSION > 2) Then
					If INCOMPLETE_PAGE > 0 Then
						session("owPageEngine").PlaceHolderOptions = CRAXDRT.CRPlaceHolderType.crAllowPlaceHolders
					End If
					If INCOMPLETE_PAGE_COUNT > 0 Then
						session("owPageEngine").PlaceHolderOptions = session("owPageEngine").PlaceHolderOptions + CRAXDRT.CRPlaceHolderType.crDelayTotalPageCountCalc
					End If
				End If
				session("owPageEngine").ImageOptions = 1
				temp = goPage.Renderepf(8209)
				'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
				Response.AddHeader("CONTENT-LENGTH", Len(temp))
				Response.ContentType = EPFMIMETYPE
				Response.binarywrite(temp)
				
			Case "HTML_FRAME"
				session("owPageEngine").ImageOptions = 1
				Response.binarywrite(goPage.Renderhtml(1, 2, 1, request.ServerVariables("SCRIPT_NAME"), 8209))
				' Need to know if it is the last page to construct the toolbar correctly
				If goPage.IsLastPage Then
					session("wLastPageNumber") = goPage.pagenumber
					session("wCurrentPageNumber") = session("wLastPageNumber")
				End If
			Case "HTML_PAGE"
				session("owPageEngine").ImageOptions = 1
				Response.binarywrite(goPage.Renderhtml(1, 3, 3, request.ServerVariables("SCRIPT_NAME"), 8209))
				
		End Select
		
	Case "GET_TTL"
		
		Call RetrieveObjects()
		
		Select Case VFMT
			
			Case "ENCP"
				Response.ContentType = ETFMIMETYPE
				If (PVERSION > 2) Then
					temp = goPageGenerator.RenderTotallerETF(gvTotallerInfo, 0, 1, gvMaxNode, 8209)
				Else
					temp = goPageGenerator.RenderTotallerETF(gvGroupPath, 0, 0, gvMaxNode, 8209)
				End If
				'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
				Response.AddHeader("CONTENT-LENGTH", Len(temp))
				Response.binarywrite(temp)
				
			Case "HTML_FRAME"
				Response.binarywrite(goPageGenerator.RenderTotallerHTML(gvGroupPath, 1, 0, gvMaxNode, gvGroupLevel, 1, request.ServerVariables("SCRIPT_NAME"), 8209))
				
		End Select
		
		
	Case "RFSH"
		
		' This command forces the database to be read again.
		session("owRpt").DiscardSavedData()
		session("owRpt").ReadRecords()
		If Err.Number <> 0 Then
			Call CheckForError()
		Else
			session("owRpt").EnableParameterPrompting = False
			session("owPageEngine") = session("owRpt").PageEngine
		End If
		Call RetrieveObjects()
		goPage = goPageCollection(PAGE)
		Call ValidatePageNumber()
		session("owPageEngine").ImageOptions = 1
		Select Case VFMT
			Case "ENCP"
				' Java and Active X Viewers will make a get page command when receiving 0 error msg value
				If VIEWER = "JAVA" Then
					session("owPageEngine").PlaceHolderOptions = 0
					If (PVERSION > 2) Then
						If INCOMPLETE_PAGE > 0 Then
							session("owPageEngine").PlaceHolderOptions = CRAXDRT.CRPlaceHolderType.crAllowPlaceHolders
						End If
						If INCOMPLETE_PAGE_COUNT > 0 Then
							session("owPageEngine").PlaceHolderOptions = session("owPageEngine").PlaceHolderOptions + CRAXDRT.CRPlaceHolderType.crDelayTotalPageCountCalc
						End If
					End If
					temp = goPage.Renderepf(8209)
					'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
					Response.AddHeader("CONTENT-LENGTH", Len(temp))
					Response.ContentType = EPFMIMETYPE
					Response.binarywrite(temp)
				Else
					Response.ContentType = EMFMIMETYPE
					session("owEMF").SendErrorMsg(0, "")
				End If
				
			Case "HTML_FRAME"
				InitializeFrameArray()
				gvURL = "htmstart.aspx"
				Response.redirect(gvURL)
				
			Case "HTML_PAGE"
				session("owPageEngine").ImageOptions = 1
				Response.binarywrite(goPage.Renderhtml(1, 3, 1, request.ServerVariables("SCRIPT_NAME"), 8209))
				
		End Select
		
		
	Case "NAV"
		Call RetrieveObjects()
		Call CheckForError()
		' Get the page number that the group in on, for this particular branch
		gvPageNumber = goPageGenerator.GetPageNumberForGroup(gvGroupLevel)
		
		Select Case VFMT
			' 0 is for epf, 8209 is a SafeArray, 8 is a BSTR
			Case "ENCP"
				' Create a byte array for the EMF, which will contain the page number
				Response.ContentType = EMFMIMETYPE
				session("owEMF").sendpagenumberrecord(gvPageNumber)
				
			Case "HTML_FRAME"
				appendQuery = "?"
				session("wCurrentPageNumber") = gvPageNumber
				If BRCH <> "" Then
					appendQuery = appendQuery & "BRCH=" & BRCH & "&"
				End If
				If GRP <> "" Then
					appendQuery = appendQuery & "GRP=" & GRP
				End If
				Response.redirect("framepage.aspx" & appendQuery)
				
		End Select
		
		
	Case "CHRT_DD"
		' only supported in java and active X smart viewers
		Select Case VFMT
			
			Case "ENCP"
				
				'  Get page collection
				Call RetrieveObjects()
				Call CheckForError()
				' Pass the coordinates to the report engine to determine what
				' branch the drill down goes to.
				goPageGeneratorDrill = goPageGenerator.DrillOnGraph(PAGE, glX, glY)
				' Check for an exception because of coordinates
				If Err.Number <> 0 Then
					gsErrorText = "Not part of the Graph "
					Response.ContentType = EMFMIMETYPE
					session("owEMF").SendErrorMsg(40, gsErrorText)
					Err.Clear()
					Response.End()
				End If
				' pass the group level and group path to helper function to create 
				' the EMF message, this tells the viewer where to get the page.
				
				gvGroupPathDD = goPageGeneratorDrill.grouppath
				gvGroupNameDD = goPageGeneratorDrill.GroupName
				Response.ContentType = EMFMIMETYPE
				session("owEMF").GroupName = gvGroupNameDD
				session("owEMF").sendbranchesemf(gvGroupPathDD)
				
				
		End Select
		
	Case "GET_LPG"
		
		' only support in smart viewers
		Select Case VFMT
			
			Case "ENCP"
				' this command returns the page number of the last page
				' Get page collection
				Call RetrieveObjects()
				Call CheckForError()
				' Get the count from the Pages collection
				gvPageNumber = goPageCollection.count
				
				' Send the EMF representing the page number
				Response.ContentType = EMFMIMETYPE
				session("owEMF").sendpagenumberrecord(gvPageNumber)
		End Select
		
	Case "SRCH"
		Call RetrieveObjects()
		Call CheckForError()
		' create page variable
		gvPageNumber = CShort(PAGE)
		
		Select Case VFMT
			Case "ENCP"
				If goPageGenerator.FindText(TEXT, 0, gvPageNumber) Then
					Response.ContentType = EMFMIMETYPE
					session("owEMF").sendpagenumberrecord(gvPageNumber)
				Else
					gsErrorText = "The specified text, '" & TEXT & "' was not found in the report"
					Response.ContentType = EMFMIMETYPE
					session("owEMF").SendErrorMsg(33, gsErrorText)
				End If
				
			Case "HTML_FRAME"
				TEXT = request.form("text")
				' Now find out what page the text is on
				tempNumber = gvPageNumber + 1
				If (CBool(goPageGenerator.FindText(TEXT, 0, tempNumber))) Then
					session("wCurrentPageNumber") = tempNumber
					searchFound = 1
				Else
					session("wCurrentPageNumber") = gvPageNumber
					searchFound = 0
				End If
				If BRCH <> "" Then
					gvURL = "framepage.aspx?brch=" & BRCH & "&SEARCHFOUND=" & searchFound
				Else
					gvURL = "framepage.aspx?SEARCHFOUND=" & searchFound
				End If
				Response.redirect(gvURL)
				
			Case "HTML_PAGE"
				' We are being called by HTML viewer
				' need to get the text from the form post
				TEXT = request.form("text")
				' Now find out what page the text is on
				tempNumber = gvPageNumber
				If (CBool(goPageGenerator.FindText(TEXT, 0, tempNumber))) Then
					gvPageNumber = tempNumber
					goPage = goPageCollection(gvPageNumber)
					session("owPageEngine").ImageOptions = 1
					Response.binarywrite(goPage.Renderhtml(1, 3, 3, request.ServerVariables("SCRIPT_NAME"), 8209))
				Else
					' Send back an html page indicating the text was not found.
					Response.Write("<html><title>Seagate ASP Reports Server</title><body bgcolor='white'><center><h1>The text cannot be found in this report.</h1></center></body></html>")
				End If
				
		End Select
		
		
	Case "TOOLBAR_PAGE"
		
		' Redirect to the framepage, need to know if we are 
		' on the last page.
		
		If CStr(session("wLastPageNumber")) <> "" Then
			If CShort(PAGE) > CShort(session("wLastPageNumber")) Then
				session("wCurrentPageNumber") = session("wLastPageNumber")
			Else
				session("wCurrentPageNumber") = PAGE
			End If
		Else
			Call RetrieveObjects()
			Call CheckForError()
			' create the actual page
			goPage = goPageCollection(PAGE)
			' check for an exception on the page number 
			Call ValidatePageNumber()
			If goPage.IsLastPage Then
				session("wLastPageNumber") = goPage.pagenumber
				session("wCurrentPageNumber") = session("wLastPageNumber")
			Else
				session("wCurrentPageNumber") = PAGE
			End If
		End If
		If BRCH <> "" Then
			gvURL = "framepage.aspx?brch=" & BRCH
		Else
			gvURL = "framepage.aspx"
		End If
		
		Response.redirect(gvURL)
		
	Case "EXPORT"
		ExportOptions = session("owRpt").ExportOptions
		If (FillExportOptionsObject(EXPORT_FMT)) Then
			Call RetrieveObjects()
			Response.binarywrite(goPageGenerator.Export(8209))
			Call CheckForError()
		Else
			Response.ContentType = EMFMIMETYPE
			session("owEMF").SendErrorMsg(1, "Invalid Export Type Specified")
		End If
		
	Case "MAP_DD"
		' only supported in java and active X smart viewers
		Select Case VFMT
			
			Case "ENCP"
				
				'  Get page collection
				Call RetrieveObjects()
				Call CheckForError()
				' Pass the coordinates to the report engine to determine what
				' branch the drill down goes to.
				goPageGeneratorDrillonMap = goPageGenerator.DrillOnMap(PAGE, glX, glY)
				' Check for an exception because of coordinates
				If Err.Number <> 0 Then
					gsErrorText = "No Values Exist for Selected Region of Map"
					Response.ContentType = EMFMIMETYPE
					session("owEMF").SendErrorMsg(40, gsErrorText)
					Err.Clear()
					Response.End()
				End If
				' pass the group level and group path to helper function to create 
				' the EMF message, this tells the viewer where to get the page.
				
				gvGroupPathDD = goPageGeneratorDrillonMap.grouppath
				gvGroupNameDD = goPageGeneratorDrillonMap.GroupName
				session("owEMF").GroupName = gvGroupNameDD
				Response.ContentType = EMFMIMETYPE
				session("owEMF").sendbranchesemf(gvGroupPathDD)
				
		End Select
		
End Select

%>







