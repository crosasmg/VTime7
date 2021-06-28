//-------------------------------------------------------------------------------------------
// ValFunctions.js:  Validaciones generales de los controles de la página
//-------------------------------------------------------------------------------------------

//-Se declara la variable mblnValid, para indicarle a las funciones cuando deben
//-validar y cuando no

   var mblnValid=true
   
//% ValText: Validaciones de los campos de texto
//-------------------------------------------------------------------------------------------
function ValText(FObjField, List, NumericText) {
//-------------------------------------------------------------------------------------------
   var lstrValue = FObjField.value
   var MaxLength = FObjField.maxLength
   var lstrAlias = FObjField.Alias
   var lintCount = 0;
   var lintCount0 = 0;
   var lintPos;
   var find = true;
   
//+Reemplaza blancos de la variable, si solo se digitaron blancos
   lstrValue =  lstrValue.replace(/(^\s*)|(\s*$)/g, "");
   FObjField.value = lstrValue;

//+El maxLength representa la cantidad maxima de caracteres permitidos, en el caso de que sea 0 entonces se asume lo definido en la propiedad size
   if (MaxLength == 0) {
       MaxLength = FObjField.size
   }

   if (!mblnValid) return 0;
   if (lstrValue != ""){
       if (typeof(MaxLength) != 'undefined') 
           if (lstrValue.length > MaxLength) {
               if (mblnShowValues){
               	//alert("La longitud del campo debe ser inferior de " + MaxLength + " caracteres")
               	alert(resValues.charsFieldLessThatMessage_10100 + MaxLength + resValues.charsFieldLessThatMessage_10101)
                   FObjField.focus();
               }
               return 0;
           }

       if(NumericText=='True') 
           List="0123456789"
           
       if ((List!="")&&(typeof(List)!='undefined')&&(List!="**")){            
           do{
               lintPos = -1;
               lintPos = List.indexOf(lstrValue.substr(lintCount,1));
               if (lintPos == -1)
                   find = false; 
               if (lstrValue.substr(lintCount,1) == '0')
                  lintCount0 = lintCount0 + 1;                                  
               lintCount++; 
           }
           while (lintCount <= lstrValue.length && find) 
                if (!find){
                    if(NumericText=='True')  
                       //alert("El campo " + lstrAlias.toLowerCase() + " debe ser numérico positivo");   
                    	alert(resValues.positiveNumericFieldMessage_10100 + lstrAlias.toLowerCase() + resValues.positiveNumericFieldMessage_10101);   
                    else
						//alert("El campo " + lstrAlias.toLowerCase() + " no acepta el caracter " + lstrValue.substr(lintCount-1,1));
                    	alert(resValues.noAllowCharFieldMessage_10100 + lstrAlias.toLowerCase() + resValues.noAllowCharFieldMessage_10101 + lstrValue.substr(lintCount - 1, 1));
                    FObjField.value = "";
                    FObjField.focus();
                }
                else{
                    if(lintCount0 == lstrValue.length){
                    	//alert("Número no válido para el campo " + lstrAlias.toLowerCase()); 
                    	alert(resValues.noAllowNumberFieldMessage + lstrAlias.toLowerCase()); 
                        FObjField.value = "";
                        FObjField.focus();    
                    }    
                }
               return 1;
       }
   }
   return 1;

}

   
//% ValNumber: Validaciones de los campos numéricos
//-------------------------------------------------------------------------------------------
function ValNumber(FObjField,ThousandSep,DecimalSep,bAllowNegativ, intDecimalPlaces){
//-------------------------------------------------------------------------------------------
	var lstrValue = FObjField.value, lintCount = 0, lintCount2 = 0, lstrAux = ""
    var lstrAlias = FObjField.Alias
    var lintShowThousand = FObjField.ShowThousand
    var lintDecPointPos  = 0
    var ldblAmount=0
    var lstrMaxAmount=""
    var ldblMaxAmount=0
    var lintHolePlace = 0
    var lstrRegExpr= ''
    var ldblMaxDecimals=0
    var ldblDecimals=0

    
    if (typeof(ThousandSep)== 'undefined') ThousandSep = ".";
    if (typeof(DecimalSep)== 'undefined') DecimalSep = ",";
    if (typeof(bAllowNegativ)=='undefined') bAllowNegativ = false;
    if (ThousandSep== '') ThousandSep = "."
    if (DecimalSep== '') DecimalSep = ","

    if (!mblnValid) return 0;
    if (typeof(lintShowThousand) == 'undefined') lintShowThousand = 0
    do {
        lstrValue = lstrValue.replace(ThousandSep,"")
    }
    while(lstrValue.indexOf(ThousandSep)>=0)
    
     do {
	    lstrValue = lstrValue.replace(" ","")
	}
	while(lstrValue.indexOf(" ")>=0)
    
    if (DecimalSep==',') lstrValue = lstrValue.replace(/\,/g,".")

//+Como la funcion isNaN toma una cadena vacia como numero valido
//+se restringue validacion
    if ( !isNaN(lstrValue) &&
         !( (lstrValue=='') && 
            (FObjField.value!='') ) ) {
       if (typeof(FObjField.HolePlace)!= 'undefined'){
           lintHolePlace = parseFloat(FObjField.HolePlace)
           for (ldblMaxAmount=0;ldblMaxAmount < lintHolePlace;ldblMaxAmount++)
               lstrMaxAmount+="9"
           if (typeof(FObjField.DecimalPlace)!= 'undefined'){
               lstrMaxAmount+=DecimalSep
               for (ldblMaxAmount=0;ldblMaxAmount < FObjField.DecimalPlace;ldblMaxAmount++)
                   lstrMaxAmount+="9"
           }
           
           ldblMaxDecimals = parseFloat(lstrMaxAmount.substr(lstrMaxAmount.indexOf(DecimalSep)+1,FObjField.DecimalPlace));
           //ldblMaxAmount = parseFloat(lstrMaxAmount.replace('.',DecimalSep))
           ldblMaxAmount = parseFloat(lstrMaxAmount.replace(DecimalSep,'.'))

           if (lstrValue.indexOf('.')>=0) ldblDecimals = parseFloat(lstrValue.substr(lstrValue.indexOf('.')+1,lstrValue.length-lstrValue.indexOf('.')));


	   if (ldblMaxAmount < parseFloat(lstrValue)){
               if (mblnShowValues)
				  //alert("El campo " + lstrAlias.toLowerCase() + " debe ser inferior de " + lstrMaxAmount);
               	alert(resValues.fieldLessThatMessage_10100 + lstrAlias.toLowerCase() + resValues.fieldLessThatMessage_10101 + lstrMaxAmount);
		       FObjField.value = ""
		       FObjField.focus();
               return 0;             
			}
			else
			    if (ldblDecimals > ldblMaxDecimals){

                   if (mblnShowValues)
						//alert("El numero de decimales del campo debe ser inferior a " + FObjField.DecimalPlace + " posiciones");
                   	   alert(resValues.decimalFieldLessThatMessage_10100 + FObjField.DecimalPlace + resValues.decimalFieldLessThatMessage_10101);
		               FObjField.value = ""
		               FObjField.focus();
                       return 0;             
			        }           
       }

       lintDecPointPos = lstrValue.indexOf(".");
       if (lintDecPointPos == -1) lintDecPointPos = lstrValue.length - 1
       else lintDecPointPos = lintDecPointPos - 1
       for (lintCount=lintDecPointPos ;lintCount>-1; lintCount--){
          if (lintCount2==3){
			 if ((lintCount == 0) &&
			     (lstrValue.substr(0,1)== '-'))
			     lintCount2=0
			 else {        
                 if(lintShowThousand!=0) lstrAux = ThousandSep + lstrAux
                 lintCount2=0
             }
          }
          lstrAux = lstrValue.substr(lintCount,1) + lstrAux;
          lintCount2++;
       }
       


       if (bAllowNegativ=='false' && parseFloat(lstrValue) < 0){
           if (mblnShowValues)
	           //alert("El campo " + lstrAlias.toLowerCase() + " debe ser mayor a cero");
           	alert(resValues.fieldGraterThatZeroMessage_10100 + lstrAlias.toLowerCase() + resValues.fieldGraterThatZeroMessage_10101);
		   FObjField.value = "";
		   FObjField.focus();
           return 0;
       }
           
       if (intDecimalPlaces==0)
		   if (lstrValue.indexOf(".")>0){
       		   //alert("El campo " + lstrAlias.toLowerCase() + " no debe tener decimales");   
       		   alert(resValues.fieldMustNoHaveDecimalMessage_10100 + lstrAlias.toLowerCase() + resValues.fieldMustNoHaveDecimalMessage_10101);   
		       FObjField.value = "";
		       FObjField.focus();
		       return false;
		   }



       ++lintDecPointPos
	   if (lintDecPointPos >= 0) 
		   FObjField.value = lstrAux + (lstrValue.substr(lintDecPointPos + 1)>''?DecimalSep:'') + lstrValue.substr(lintDecPointPos + 1)
       else 
           FObjField.value = lstrAux
       return true;
    }
	else{
		if (mblnShowValues) {
			//alert("El campo " + lstrAlias.toLowerCase() + " debe ser numérico");
			alert(resValues.fieldMustBeNumericMessage_10100 + lstrAlias.toLowerCase() + resValues.fieldMustBeNumericMessage_10101);
		}
		FObjField.value = ""
        FObjField.focus();
		return false;
	}
}
   
//% ValDate: Validaciones de los campos de fecha
//-------------------------------------------------------------------------------------------
function ValDate(FObjField,sUserDateFormat,sUserDateSeparator) {
//-------------------------------------------------------------------------------------------
    var lstrValue = FObjField.value
    var lstrValue1 = FObjField.value
    var lstrAlias = FObjField.Alias
    var lintMonth = 0, lintDay = 0, lintYear = 0, ldtmDate, lintYear2 =  0
	var lintPosDay = 0, lintPosMonth = 0, lintPosYear = 0
	var lintindex = 0, lstrPosDateFormat = "DMY"
	var lintVal1 = 0, lintVal2 = 0, lintVal3 = 0, lintCountDateSeparator = 0
	var lintindex1 = 0
	
//+ Se obtiene la posicion del día, mes y año dependiendo del formato
	if (typeof(sUserDateFormat)!='undefined'){
		do {
			if ((sUserDateFormat.substr(lintindex,1).toUpperCase() == 'D')&&(lintPosDay == 0)){
				lintPosDay = 4 - lstrPosDateFormat.length
				lstrPosDateFormat = lstrPosDateFormat.replace("D","")
			}
			
			if ((sUserDateFormat.substr(lintindex,1).toUpperCase() == 'M')&&(lintPosMonth == 0)){
				lintPosMonth = 4 - lstrPosDateFormat.length
				lstrPosDateFormat = lstrPosDateFormat.replace("M","")
			}
			
			if ((sUserDateFormat.substr(lintindex,1).toUpperCase() == 'Y')&&(lintPosYear == 0)){
				lintPosYear = 4 - lstrPosDateFormat.length
				lstrPosDateFormat = lstrPosDateFormat.replace("Y","")
			}
			
			lintindex++
		}
		while (lintindex <= sUserDateFormat.length)
	}
	
    if (!mblnValid) return 0;
    
    if(lstrValue == "") return 1;
    
    lintindex = 0
    
    if ((lstrValue != "")&&(typeof(sUserDateSeparator)!='undefined')){
        do {
            lstrValue = lstrValue.replace(" ","")
        }
        while (lstrValue != lstrValue.replace(" ",""))
        
        do {
			lstrValue = lstrValue.replace(sUserDateSeparator,"")
			lintindex++
        }
        while (lstrValue != lstrValue.replace(sUserDateSeparator,""))
		
		
		if (lintindex > 1){
			lintindex = 0
			do {
				if (lstrValue1.substr(lintindex,1) == sUserDateSeparator){
					lintCountDateSeparator++
					if (lintCountDateSeparator == 1){
						lintVal1 = parseFloat(lstrValue1.substr(0,lintindex))
						lintindex1 = lintindex + 1
					}
					if (lintCountDateSeparator == 2){
						lintVal2 = parseFloat(lstrValue1.substr(lintindex1,lintindex-lintindex1))
						lintindex1 = lintindex+1
						lintVal3 = parseFloat(lstrValue1.substr(lintindex1,lstrValue1.length-lintindex1))
					}
				}
				lintindex++
			}
			while (lintindex <= lstrValue1.length)

			if (lintPosDay == 1) lintDay = lintVal1
			else{
				if (lintPosDay == 2) lintDay = lintVal2
				else{
					if (lintPosDay == 3) lintDay = lintVal3
				}
			}

			if (lintPosMonth == 1) lintMonth = lintVal1
			else{
				if (lintPosMonth == 2) lintMonth = lintVal2
				else{
					if (lintPosMonth == 3) lintMonth = lintVal3
				}
			}
			
			if (lintPosYear == 1) lintYear = lintVal1
			else{
				if (lintPosYear == 2) lintYear = lintVal2
				else{
					if (lintPosYear == 3) lintYear = lintVal3
				}
			}
		}
		else{
			if (lstrValue.length > 1) lintDay = parseFloat(lstrValue.substr(0,2))
			else lintDay = 0
			if (lstrValue.length > 3) lintMonth = parseFloat(lstrValue.substr(2,2))
			else lintMonth = 0
			if (lstrValue.length > 4) lintYear = parseFloat(lstrValue.substr(4))
			else lintYear = 0
		}

		if (!isNaN(lstrValue)){
			if ((lintYear > 50)&&(lintYear<100)) lintYear += 1900
			if (lintYear < 1000) lintYear += 2000
			if ((lintDay != 0)&&(lintMonth != 0)){
			    lstrValue = ""
			    
			    if (lintPosDay == 1){
					if (lintDay < 10 )
					    lstrValue += "0" + lintDay
					else
					    lstrValue += lintDay
				}
				else{
					if (lintPosMonth == 1){
					    if (lintMonth < 10 )
						    lstrValue += "0" + lintMonth
						else
						    lstrValue += lintMonth
					}
					else{
						if (lintPosYear == 1)
							lstrValue += lintYear
					}
				}
				
			    if (lintPosDay == 2){
					if (lintDay < 10 )
					    lstrValue += sUserDateSeparator + "0" + lintDay
					else
					    lstrValue += sUserDateSeparator + lintDay
				}
				else{
					if (lintPosMonth == 2){
					    if (lintMonth < 10 )
						    lstrValue += sUserDateSeparator + "0" + lintMonth
						else
						    lstrValue += sUserDateSeparator + lintMonth
					}
					else{
						if (lintPosYear == 2)
							lstrValue += sUserDateSeparator + lintYear
					}
				}

			    if (lintPosDay == 3){
					if (lintDay < 10 )
					    lstrValue += sUserDateSeparator + "0" + lintDay
					else
					    lstrValue += sUserDateSeparator + lintDay
				}
				else{
					if (lintPosMonth == 3){
					    if (lintMonth < 10 )
						    lstrValue += sUserDateSeparator + "0" + lintMonth
						else
						    lstrValue += sUserDateSeparator + lintMonth
					}
					else{
						if (lintPosYear == 3)
							lstrValue += sUserDateSeparator + lintYear
					}
                }

                ldtmDate = new Date(lintYear, (lintMonth - 1), lintDay, 6, 0, 0, 0)
			    lintYear2 = ldtmDate.getFullYear()
			    if (lintYear2< 100) lintYear2 += 1900
			    if ((lintDay   != ldtmDate.getDate())     ||
			        (lintMonth != ldtmDate.getMonth() + 1)||
			        (lintYear  != lintYear2)              ||
			        (lintYear >= 10000))
			    {
			        lstrValue = ""
			        if (mblnShowValues) {
			        	//alert ("Fecha no lógica: " + sUserDateFormat)
			        	alert(resValues.noLogicDateCaption + sUserDateFormat);
			        }
			        FObjField.value = ""
			        FObjField.focus();
			        return 0;
			    }    
			}
			else{
					lstrValue = ""
					if (mblnShowValues) {
						//alert ("Fecha no lógica: " + sUserDateFormat)
						alert(resValues.noLogicDateCaption + sUserDateFormat);
					}
					FObjField.value = ""
					FObjField.focus();
					return 0;
			}
		}
		else{
			    lstrValue = ""
			    if (mblnShowValues) {
			    	//alert ("Fecha no lógica: " + sUserDateFormat)
			    	alert(resValues.noLogicDateCaption + sUserDateFormat);
			    }
			    FObjField.value = ""
			    FObjField.focus();
			    return 0;
		}
		FObjField.value = lstrValue
		return 1;
    }
}

//% ValFields: 
//-------------------------------------------------------------------------------------------
function ValFields(){
//-------------------------------------------------------------------------------------------
   var lintBreak, linIndex, lstrName
   lintBreak = 0
   for (lintIndex = 0; lintBreak == 0; lintIndex++){
       if (typeof(document.forms[0].elements[lintIndex]) == 'undefined')
          lintBreak = 1
       else {
          lstrName = document.forms[0].elements[lintIndex].name;
          if (lstrName.substr(0,3) == 'tcn') 
              alert("es Numero")
          else {
              if (lstrName.substr(0,3) == 'tct') 
                  ValText(document.forms[0].elements[lintIndex])
              else 
                  if (lstrName.substr(0,3) == 'tcd') 
                      alert("es Fecha")
          }
       }          
   }
}

//% OpenCalendar: Esta funcion se encarga de mostrar la forma del calendario
//-------------------------------------------------------------------------------------------
function OpenCalendar(tcdField,nParentForm,sUserDateFormat,sUserDateSeparator) {
//-------------------------------------------------------------------------------------------
    var ldtmDate=""
    if (!tcdField.disabled){
        ValDate(tcdField);
        ldtmDate = tcdField.value
        if (ldtmDate != "") ldtmDate = "?CurDate=" + ldtmDate.substr(0,2) + sUserDateSeparator + ldtmDate.substr(3,2) + sUserDateSeparator + ldtmDate.substr(6,4)
        if (ldtmDate != "") ldtmDate += "&FieldName=" + tcdField.name
        else ldtmDate += "?FieldName=" + tcdField.name
        ShowPopUp("/VTimeNet/Common/Calendar.aspx" + ldtmDate ,"Calendar",255,320)
    }
    else
       return;
}

//% ChangeFocus: selecciona el primer control habilitado de la página
//-------------------------------------------------------------------------------------------
function ChangeFocus(Field){
//-------------------------------------------------------------------------------------------    
    if (Field.disabled)
        field.blur()
} 
//-------------------------------------------------------------------------------------------    
function SetParameters(Field){
//-------------------------------------------------------------------------------------------    
   var lintIndex = 0
   var lstrBlur  = ""
   var lobjField
   mblnValid=false
   for (lintIndex=0;lintIndex<=(document.forms[0].elements.length - 1);lintIndex++)
   {
       lstrBlur = ""
	   lstrBlur += $(document.forms[0].elements[lintIndex]).attr("onBlurCode")
       if (lstrBlur.indexOf(Field.name + ".Parameters.Param")>=0) {$(document.forms[0].elements[lintIndex]).change()}
   }
   mblnValid=true
}
//-------------------------------------------------------------------------------------------    
function DoBlur(Field){
//-------------------------------------------------------------------------------------------    
    var lstrBlur  = ""
    var lobjField
}
//-------------------------------------------------------------------------------------------    
function DoBlurParam(Field){
//-------------------------------------------------------------------------------------------    
    var lstrBlur  = ""
    var lobjField
    var lintIndex=0;
    mblnShowValues=false;
    lstrBlur = ""
    for (lintIndex=0;lintIndex<document.forms[0].elements.length;lintIndex++){
        if (document.forms[0].elements[lintIndex].name!=Field.name){
            if (typeof(document.forms[0].elements[lintIndex].onblur)=='function'){
                lstrBlur += document.forms[0].elements[lintIndex].onblur
                if (document.forms[0].elements[lintIndex].value != '')
                    if (lstrBlur.indexOf(".Parameters.Param")>=0) {
                    }
                lstrBlur=""
            }
            else
                lintIndex=document.forms[0].elements.length
        }
    }
    mblnShowValues=true;
}

//-----------------------------------------------------
//---- FUNCIONES DE LOS COMBOS ------------------------

var toFind = "";              // Variable that acts as keyboard buffer
var timeoutID = "";           // Process id for timer (used when stopping 
                              // the timeout)
timeoutInterval = 250;        // Milliseconds. Shorten to cause keyboard 
                              // buffer to be cleared faster
var timeoutCtr = 0;           // Initialization of timer count down
var timeoutCtrLimit = 3 ;     // Number of times to allow timer to count 
                              // down
var oControl = "";            // Maintains a global reference to the 
function listbox_onkeypress(){
   // This function is called when the user presses a key while focus is in 
   // the listbox. It maintains the keyboard buffer.
   // Each time the user presses a key, the timer is restarted. 
   // First, stop the previous timer; this function will restart it.
   window.clearInterval(timeoutID)

   // Which control raised the event? We'll need to know which control to 
   // set the selection in.
   oControl = window.event.srcElement;

   var keycode = window.event.keyCode;
   if(keycode >= 32 ){
       // What character did the user type?
       var c = String.fromCharCode(keycode);
       c = c.toUpperCase(); 
       // Convert it to uppercase so that comparisons don't fail
       toFind += c ; // Add to the keyboard buffer
       find();    // Search the listbox
       timeoutID = window.setInterval("idle()", timeoutInterval);  
       // Restart the timer
       if (oControl.onchange!= null)
           oControl.onchange();
    }
}
function listbox_onblur(){
   // This function is called when the user leaves the listbox.

   window.clearInterval(timeoutID);
   resetToFind();
}
function idle(){
   // This function is called if the timeout expires. If this is the 
   // third (by default) time that the idle function has been called, 
   // it stops the timer and clears the keyboard buffer

   timeoutCtr += 1
   if(timeoutCtr > timeoutCtrLimit){
      resetToFind();
      timeoutCtr = 0;
      window.clearInterval(timeoutID);
   }
}

function resetToFind(){
   toFind = ""
}
function find(){
    // Walk through the select list looking for a match

    var allOptions = document.all.item(oControl.name);

    for (i=0; i < allOptions.length; i++){
       // Gets the next item from the listbox
       nextOptionText = allOptions(i).text.toUpperCase();

       // By default, the values in the listbox and as entered by the  
       // user are strings. This causes a string comparison to be made, 
       // which is not correct for numbers (1 < 11 < 2).
       // The following lines coerce numbers into an (internal) number 
       // format so that the subsequent comparison is done as a 
       // number (1 < 2 < 11).

       if(!isNaN(nextOptionText) && !isNaN(toFind) ){
              nextOptionText *= 1;        // coerce into number
              toFind *= 1;
       }

        // Does the next item match exactly what the user typed?
        if(toFind == nextOptionText){
            // OK, we can stop at this option. Set focus here
            oControl.selectedIndex = i;
            window.event.returnValue = false;
            break;
        }

        // If the string does not match exactly, find which two entries 
        // it should be between.
        if(i < allOptions.length-1){

           // If we are not yet at the last listbox item, see if the 
           // search string comes between the current entry and the next 
           // one. If so, place the selection there.

           lookAheadOptionText = allOptions(i+1).text.toUpperCase() ;
           if( (toFind > nextOptionText) && 
              (toFind < lookAheadOptionText) ){
              oControl.selectedIndex = i+1;
              window.event.cancelBubble = true;
               window.event.returnValue = false;
              break;
           } // if
           } // if

        else{

           // If we are at the end of the entries and the search string 
           // is still higher than the entries, select the last entry

           if(toFind > nextOptionText){
               oControl.selectedIndex = allOptions.length-1 // stick it 
                                                            // at the end
               window.event.cancelBubble = true;
               window.event.returnValue = false;
               break;
           } // if
       } // else
    }  // for
} // function