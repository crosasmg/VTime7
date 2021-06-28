<%@ Page language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de Values
Dim mobjValues As eFunctions.Values

'- Objetos para el manejo de los Values a Graficar
Dim mobjProjectVul As ePolicy.Projectvul
Dim mobjProjectVuls As ePolicy.Projectvuls

'- Variables para almacenar parametros del Gráfico
Dim mstrCertype As String
Dim mintBranch As String
Dim mintProduct As String
Dim mlngPolicy As String
Dim mlngCertif As String
Dim mstrProduct As String
Dim mintChartTyp As String
Dim mstrCurrency As String
Dim mstrDescript As String
Dim mstrTitle As String

'+ Variables para el manejo de la data a graficar
Dim cd As Object
Dim c As Object
Dim V(300) As Double
Dim Pointer(300) As Double
Dim data0() As Double
Dim data1() As Double
Dim data2() As Double
Dim Step_val As Byte
Dim layer As Object
Dim MaxAge As Double
Dim total As Integer
Dim MinAge As Double
Dim labels() As String
Dim Delta As Double
Dim Values As String
Dim j As Integer
Dim i As Double
Dim totalV As Double
Dim pos As Double
Dim Position As Double
Dim Exist As Boolean
Dim filename As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjProjectVul = New ePolicy.Projectvul
mobjProjectVuls = New ePolicy.Projectvuls

'+ Se asignan Values de parámetros     
mstrCertype = Request.QueryString.Item("sCertype")
mintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), 4)
mintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), 4)
mlngPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), 4)
mlngCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), 4)
mstrProduct = Request.QueryString.Item("sProduct")
mintChartTyp = Request.QueryString.Item("nChartTyp")
mstrCurrency = Request.QueryString.Item("sCurrency")

'Se realiza la busqueda de los Datos a Graficar
If mobjProjectVuls.Find(mstrCertype, mobjValues.StringToType(mintBranch, 4), mobjValues.StringToType(mintProduct, 4), mobjValues.StringToType(mlngPolicy, 4), mobjValues.StringToType(mlngCertif, 4), mobjValues.StringToType(CStr(Today), 1)) Then
	
	total = mobjProjectVuls.Count
	
	MinAge = mobjProjectVuls.Item(1).nAge
	MaxAge = mobjProjectVuls.Item(total).nAge
	Step_val = 10
	
	Delta = System.Math.Round((MaxAge - MinAge) / Step_val, 0)
	i = 1
	V(i) = MinAge
	While (V(i) <= CShort(MaxAge))
		i = i + 1
		V(i) = V(i - 1) + Delta
		Values = Values & V(i - 1) & ","
	End While
	
	If CShort(V(i)) > CShort(MaxAge) Then
		V(i) = MaxAge
		Values = Values & V(i) & ","
		totalV = i '+1   
	Else
		totalV = i
	End If
	
	pos = 1
	i = 2
	For j = 1 To totalV
		Exist = False
		i = i - 1
		While Not (Exist)
			If CShort(V(j)) = CShort(mobjProjectVuls.Item(i).nAge) Then
				Position = i
				Exist = True
			End If
			i = i + 1
		End While
		If Exist Then
			Pointer(pos) = Position
			pos = pos + 1
		End If
	Next 
	
	Values = Mid(Values, 1, Len(Values) - 1) 'Edades 
	
	ReDim data0(pos - 1)
	For i = 1 To pos - 1
		data0(i) = System.Math.Round(CDbl(mobjValues.TypeToString(mobjProjectVuls.Item(Pointer(i)).nPremium, 3))) 'Prima Acumulada 
	Next 
	
	ReDim data1(pos - 1)
	For i = 1 To pos - 1
		data1(i) = System.Math.Round(CDbl(mobjValues.TypeToString(mobjProjectVuls.Item(Pointer(i)).nVp, 3))) 'Valor Poliza 
	Next 
	
	ReDim data2(pos - 1)
	If mintChartTyp = "1" Then
		
		data2(0) = System.Math.Round(CDbl(mobjValues.TypeToString(mobjProjectVuls.Item(Pointer(1)).nCapital, 3))) ' Beneficio por Fallecimiento
		
		For i = 1 To pos - 1
			data2(i) = System.Math.Round(CDbl(mobjValues.TypeToString(mobjProjectVuls.Item(Pointer(i)).nCapital, 3))) ' Beneficio por Fallecimiento
		Next 
		mstrDescript = "Monto Asegurado"
		mstrTitle = "Proyección de Monto Asegurado"
	Else
		For i = 1 To pos - 1
			data2(i) = System.Math.Round(CDbl(mobjValues.TypeToString(mobjProjectVuls.Item(Pointer(i)).nSurramount, 3))) ' Valor de Rescate
		Next 
		mstrDescript = "Valor de Rescate"
		mstrTitle = "Proyección Valor de Rescate"
	End If
End If

mobjProjectVuls = Nothing

labels = Values.Split(",")

'+ Se instancia el objeto encargado de Generar el Gráfico
'UPGRADE_NOTE: The 'ChartDirector.API' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
cd = CreateObject("ChartDirector.API")
cd.setLicenseCode("RDST-245A-FWZC-5C8S-1111-D2C9")

'Create a XYChart object of size 450 x 250 pixels, with a pale blue (&HCCCCFF) 
'background, a black border, and 1 pixel 3D border effect. 
c = cd.XYChart(500, 300, &HCCCCFF, 0, 1)

'Set the plotarea at (60, 45) and of size 360 x 170 pixels, using white 
'(0xffffff) as the plot area background color. Turn on both horizontal and 
'vertical grid lines with light grey color (0xc0c0c0) 
Call c.setPlotArea(80, 45, 360, 200, &HFFFFFF, -1, -1, &HC0C0C0, -1)

'Add a legend box at (60, 20) (top of the chart) with horizontal layout. Use 8 
'pts Arial Bold font. Set the background and border color to Transparent. 
Call c.addLegend(60, 20, False, "arialbd.ttf", 8).setBackground(cd.Transparent)

'Add a title to the chart using 12 pts Arial Bold/white font. Use a 1 x 2 bitmap 
'pattern as the background. 
Call c.addTitle(mstrTitle, "arialbd.ttf", 12, &HFFFFFF, &H66s)

'Set the labels on the x axis 
Call c.xAxis().setLabels(labels)

'Reserve 8 pixels margins at both side of the x axis to avoid the first and last 
'symbols drawing outside of the plot area 
Call c.xAxis().setMargin(8, 8)

'Add a title to the y axis 
Call c.yAxis().setTitle(mstrCurrency)

'Reserve 8 pixels margins at both side of the y axis to avoid the first and last 
'symbols drawing outside of the plot area 
Call c.yAxis().setMargin(16, 16)

'Add a title to the x axis 
Call c.xAxis().setTitle("Edad Alcanzada del Asegurado")

'Add a line layer to the chart 
layer = c.addLineLayer2()

'Set the line width to 2 pixels 
Call layer.setLineWidth(2)

'Call c.setSearchPath(Server.MapPath(".")) 

'Add the first line using small_user.png as the symbol. 
Call layer.addDataSet(data0, &HCF4040, "Prima Acumulada")
'.setDataSymbol2(Server.MapPath("small_user.png")) 

'Add the first line using small_computer.png as the symbol. 
Call layer.addDataSet(data1, &HFFFF33, "Valor Póliza")
'.setDataSymbol2( Server.MapPath("small_computer.png")) 

'Add the first line using data2.png as the symbol. 
Call layer.addDataSet(data2, &HFFs, mstrDescript)
'.setDataSymbol2( Server.MapPath("data2.png")) 

'filename=c.maketmpFile(Server.Mappath("/TFiles")) 
'filename="E:\VisualTime\VTime\TFiles\chart_" & mlngPolicy & ".png" 
'c.makeChart(filename) 

'output the chart 
Response.ContentType = "image/png"

Response.BinaryWrite(c.makeChart2(cd.PNG))
Response.End()

%> 





