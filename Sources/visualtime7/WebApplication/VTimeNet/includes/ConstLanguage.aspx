<script language="VB" runat="Server">

Const C_CALENDAR As String = "Calendario"
Const C_FINDDATE As String = "Indique la fecha de búsqueda"
Const C_LOOKUP As String = "Valores posibles"
Const C_ERRORS As String = "Errores/advertencias encontrados"
Const C_GE010 As String = "Valores posibles de póliza"

'- Mensajes de error 
'+ (Por favor agregar en orden ascendente, según el nro. de error)
Const C_MESSAGE_1068 As String = "Debe indicar la condición de búsqueda"
Const C_MESSAGE_2090 As String = "Debe corresponder con la estructura  Rut - digito verificador"
Const C_MESSAGE_3301 As String = "Existen certificados asociados a la situación"
Const C_MESSAGE_4116 As String = "Código de profesional, no esta registrado"
Const C_MESSAGE_4332 As String = "No puede eliminar el caso, tiene información asociada"
Const C_MESSAGE_10214 As String = "Indique el tipo de registro"
Const C_MESSAGE_12043 As String = "Incluya código del cliente"
Const C_MESSAGE_12103 As String = "Transacción no permitida para su esquema"
Const C_MESSAGE_34052 As String = "El asegurado posee otros siniestros declarados"
Const C_MESSAGE_55031 As String = "Debe indicar el área de seguro"
Const C_MESSAGE_55127 As String = "El monto a pagar no debe ser mayor al saldo pendiente del préstamo/anticipo"
Const C_MESSAGE_55145 As String = "Propuesta ya tiene generada la orden de pago"
Const C_MESSAGE_55146 As String = "No corresponde devolución, no se generará orden de pago"
Const C_MESSAGE_55783 As String = "Vehículo ya asegurado, verifique si corresponde pago de comisión a intermediario"
Const C_MESSAGE_3790 As String = "Propuesta : Ya esta registrada en otra póliza"
Const C_MESSAGE_55893 As String = "No se puede eliminar el grupo, tiene información asociada"
Const C_MESSAGE_55908 As String = "Fecha debe ser posterior a última fecha registrada para la clasificación SVS"
Const C_MESSAGE_55909 As String = "No puede realizar modificación para esta fecha ya se ha ejecutado margen de solvencia"
Const C_MESSAGE_55947 As String = "No se generará recibo de devolución"
Const C_MESSAGE_55983 As String = "No se pudo calcular el dígito de la patente, verifique la serie"
Const C_MESSAGE_56000 As String = "No se puede eliminar/modificar el ""Asegurado Principal"""
Const C_MESSAGE_56037 As String = "No puede eliminar registro, ya tiene ajustes realizados"
Const C_MESSAGE_60254 As String = "Existen doctos. pendientes de cobro para pólizas involucradas en la devolución"
Const C_MESSAGE_60583 As String = "Ya se ha generado un recibo automático para el endoso"
Const C_MESSAGE_60584 As String = "Ya se ha generado un recibo manual para el endoso"
Const C_MESSAGE_60586 As String = "No se puede eliminar registros no vigentes"
Const C_MESSAGE_98033 As String = "La longitud no es válida según el tipo de campo"
Const C_MESSAGE_99004 As String = "Se debe indicar la ventana a llamar"
Const C_MESSAGE_99041 As String = "No existe un registro para la numeracion automatica del elemento"
Const C_MESSAGE_56168 As String = "No se puede eliminar. Orden de servicio tiene infromación asociada."
Const C_MESSAGE_80000 As String = "No puede elimar una columna que pertenece a una tabla lógica de tarifa."


</script>




