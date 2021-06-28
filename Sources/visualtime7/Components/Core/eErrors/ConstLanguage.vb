Option Strict Off
Option Explicit On
Module ConstLanguage
	
	'-Valores según Table563
	Public Const C_ADD As String = "Agregar"
	Public Const C_DEL As String = "Eliminar"
	Public Const C_NOTEGENERALINFO As String = "Información general de la nota"
	Public Const C_INFODELETED As String = "Información eliminada"
	Public Const C_DATANOTFOUND As String = "No existen registros a mostrar"
	Public Const C_GOTOMODULE As String = "Ir a módulo de"
	
	Public Const C_ACCEPTINFWINDOWS As String = "Aceptar la información de la ventana"
	Public Const C_CANCELINFWINDOWS As String = "Cancelar la información de la ventana"
	Public Const C_PREVIOUSRECORD As String = "Ir al registro anterior"
	Public Const C_NEXTRECORD As String = "Ir al siguiente registro"
	Public Const C_GOINIPAGE As String = "Ir al inicio de la página"
	
	Public Const C_POSSIBLEVALUES As String = "Valores posibles"
	Public Const C_ABOUT As String = "Acerca de..."
	Public Const C_GOTO As String = "Ir a..." '10401
	Public Const C_EXITSYS As String = "Salir del sistema" '10405
	Public Const C_QUESYS As String = "Consulta general del sistema" '10406
	
	Public Const C_POLICY As String = "Póliza" '4
	Public Const C_PROPOSAL As String = "Propuesta" '5
	Public Const C_QUOTATION As String = "Cotización" '6
	Public Const C_QUOTAMEND As String = "Cotización de modificación" '51
	Public Const C_QUOTRENEW As String = "Cotización de renovación" '52
	Public Const C_PROPAMEND As String = "Solicitud de Endoso" '53
	Public Const C_PROPRENEW As String = "Propuesta de renovación" '54
	Public Const C_PROPREHAB As String = "Propuesta de rehabilitación" '54
	
	Public Const C_DATE As String = "Fecha" '110
	
	Public Const C_ISSUE As String = "Emisión" '209
	Public Const C_RECEPTION As String = "Recepción" '210
	Public Const C_EXPIRATION As String = "Vencimiento" '211
	
	Public Const C_COMMISPER As String = "% de Comisión" '113
	Public Const C_PERCENT As String = "Porcentaje" '217
	Public Const C_NUMBER As String = "Número" '1013
	Public Const C_ENDPREM As String = "Prima final"
	Public Const C_ONCOMMIS As String = "Sobre-comisión"
	
	Public Const C_DATASHEET As String = "Datos de la hoja"
	Public Const C_GROUPLOAD As String = "Plantilla de carga"
	Public Const C_LIST As String = "Lista"
	
	Public Const C_INCAP As String = "Incapacidad"
	Public Const C_DEATHACC As String = "Muerte en accidente"
	Public Const C_DEATHCIR As String = "Muerte en circulación"
	Public Const C_DISSAB As String = "Invalidez"
	
	Public Const C_CHILDS As String = "Hijos"
	Public Const C_CARS As String = "Autos"
	Public Const C_WEIGHT As String = "Peso"
	Public Const C_HEIGHT As String = "Altura"
	
	Public Const C_USER As String = "Usuario" '1044
	
	Public Const CT_ERR As String = "Err. "
	Public Const CT_ADV As String = "Adv. "
	Public Const CT_MEN As String = "Men. "
	Public Const CT_NOMENCO As String = "No se encuentra el mensaje correspondiente"
	Public Const CT_NOTRANSAC As String = "No se encontró la información de la transacción"
	Public Const CT_SEQUENCE As String = "Secuencia"
	
	Public Const C_MON As String = "Lun"
	Public Const C_TUE As String = "Mar"
	Public Const C_WED As String = "Mie"
	Public Const C_THU As String = "Jue"
	Public Const C_FRI As String = "Vie"
	Public Const C_SAT As String = "Sab"
	Public Const C_SUN As String = "Dom"
	Public Const C_CODE As String = "Código"
	Public Const C_DESCRIPT As String = "Descripción"
	Public Const C_QASSOCIATE As String = "Consultas Asociadas"
	
	Public Const CN_NOTEMPTY As String = "Con contenido"
	
	Public Const CN_NOCLASSPROG As String = "La clase asociada a la carpeta no está programada"
	
	Public Const C_DATLOADERR As String = "Error al cargar datos"
	
	Public Const C_PARTICIP As String = "Participación"
	
	Public Const C_TCERT As String = "el certificado"
	Public Const C_TQUOTCERT As String = "la cotización del certificado"
	Public Const C_TPROPOCERT As String = "la solicitud del certificado"
	Public Const C_TPOLICY As String = "la póliza"
	Public Const C_TQUOTATION As String = "la cotización"
	Public Const C_TPROPOSAL As String = "la solicitud"
	
	Public Const C_RATE As String = "Tasa"
	Public Const C_PERINCREASE As String = "% Máx. Aumentar"
	Public Const C_PERDIMINISH As String = "% Máx. Disminuir"
	Public Const C_TOINCLUDE As String = "incluir"
	Public Const C_TOEXCLUDE As String = "excluir"
	Public Const C_O As String = "o"
	Public Const C_TMUST As String = "debe"
	
	Public Const C_INCREASE As String = "Aumentar"
	Public Const C_DIMINISH As String = "Disminuir"
	Public Const C_OPTIONAL As String = "Opcional"
	Public Const C_HIDE As String = "Oculta"
	Public Const C_REQUIRED As String = "Requerida"
	Public Const C_SUM As String = "Suma"
	Public Const C_PERMIN As String = "Porcentaje mínimo"
	Public Const C_PERINV As String = "Porcentaje de inversión"
	Public Const C_COSTPUR As String = "Costo de compra"
	Public Const C_COSTSAL As String = "Costo de venta"
	Public Const C_INDIV As String = "Individual"
	Public Const C_COLPOLMAT As String = "Colectiva-Póliza matriz"
	Public Const C_MULPOLMAT As String = "Multilocalidad-Póliza matriz"
	Public Const C_PERDIS As String = "% de descuento"
	Public Const C_MONTHS As String = "Meses"
	Public Const C_DAYS As String = "Días"
	
	Public Const C_TPARAM As String = "El parámetro"
	Public Const C_TVALUEPARAM As String = "El valor del parámetro"
	Public Const C_OBLIGAT As String = "es obligatorio"
	Public Const C_NOTDATEVAL As String = "no es una fecha válida"
	Public Const C_TNOTNUMBER As String = "no es número"
	
	Public Const C_MONTHNEXT As String = "Mes siguiente"
	Public Const C_MONTHPREV As String = "Mes anterior"
	
	Public Const C_SHOWDOCTRAN As String = "Muestra el funcional de la transacción"
	
	Public Const C_TQUERYS As String = "Consultas"
	
	Public Const C_CONTINUE As String = "Continuar"
	Public Const C_TCONTINUE As String = "Continuar con la acción en tratamiento"
	
	Public Const C_TNAME As String = "Nombre"
	Public Const C_TVALUE As String = "Valor"
	Public Const C_TMESSAGE As String = "Mensaje"
	
	Public Const C_TSTATUS As String = "Estado"
	Public Const C_TRESOURCE As String = "Recurso"
	
	Public Const C_TSHOWCONDMAX As String = "Sólo se mostraran los primeros 200 elementos resultantes de la condición"
End Module










