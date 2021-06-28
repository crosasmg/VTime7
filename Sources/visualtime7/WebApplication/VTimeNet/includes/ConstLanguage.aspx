<script language="VB" runat="Server">

Const C_CALENDAR As String = "Calendario"
Const C_FINDDATE As String = "Indique la fecha de b�squeda"
Const C_LOOKUP As String = "Valores posibles"
Const C_ERRORS As String = "Errores/advertencias encontrados"
Const C_GE010 As String = "Valores posibles de p�liza"

'- Mensajes de error 
'+ (Por favor agregar en orden ascendente, seg�n el nro. de error)
Const C_MESSAGE_1068 As String = "Debe indicar la condici�n de b�squeda"
Const C_MESSAGE_2090 As String = "Debe corresponder con la estructura  Rut - digito verificador"
Const C_MESSAGE_3301 As String = "Existen certificados asociados a la situaci�n"
Const C_MESSAGE_4116 As String = "C�digo de profesional, no esta registrado"
Const C_MESSAGE_4332 As String = "No puede eliminar el caso, tiene informaci�n asociada"
Const C_MESSAGE_10214 As String = "Indique el tipo de registro"
Const C_MESSAGE_12043 As String = "Incluya c�digo del cliente"
Const C_MESSAGE_12103 As String = "Transacci�n no permitida para su esquema"
Const C_MESSAGE_34052 As String = "El asegurado posee otros siniestros declarados"
Const C_MESSAGE_55031 As String = "Debe indicar el �rea de seguro"
Const C_MESSAGE_55127 As String = "El monto a pagar no debe ser mayor al saldo pendiente del pr�stamo/anticipo"
Const C_MESSAGE_55145 As String = "Propuesta ya tiene generada la orden de pago"
Const C_MESSAGE_55146 As String = "No corresponde devoluci�n, no se generar� orden de pago"
Const C_MESSAGE_55783 As String = "Veh�culo ya asegurado, verifique si corresponde pago de comisi�n a intermediario"
Const C_MESSAGE_3790 As String = "Propuesta : Ya esta registrada en otra p�liza"
Const C_MESSAGE_55893 As String = "No se puede eliminar el grupo, tiene informaci�n asociada"
Const C_MESSAGE_55908 As String = "Fecha debe ser posterior a �ltima fecha registrada para la clasificaci�n SVS"
Const C_MESSAGE_55909 As String = "No puede realizar modificaci�n para esta fecha ya se ha ejecutado margen de solvencia"
Const C_MESSAGE_55947 As String = "No se generar� recibo de devoluci�n"
Const C_MESSAGE_55983 As String = "No se pudo calcular el d�gito de la patente, verifique la serie"
Const C_MESSAGE_56000 As String = "No se puede eliminar/modificar el ""Asegurado Principal"""
Const C_MESSAGE_56037 As String = "No puede eliminar registro, ya tiene ajustes realizados"
Const C_MESSAGE_60254 As String = "Existen doctos. pendientes de cobro para p�lizas involucradas en la devoluci�n"
Const C_MESSAGE_60583 As String = "Ya se ha generado un recibo autom�tico para el endoso"
Const C_MESSAGE_60584 As String = "Ya se ha generado un recibo manual para el endoso"
Const C_MESSAGE_60586 As String = "No se puede eliminar registros no vigentes"
Const C_MESSAGE_98033 As String = "La longitud no es v�lida seg�n el tipo de campo"
Const C_MESSAGE_99004 As String = "Se debe indicar la ventana a llamar"
Const C_MESSAGE_99041 As String = "No existe un registro para la numeracion automatica del elemento"
Const C_MESSAGE_56168 As String = "No se puede eliminar. Orden de servicio tiene infromaci�n asociada."
Const C_MESSAGE_80000 As String = "No puede elimar una columna que pertenece a una tabla l�gica de tarifa."


</script>




