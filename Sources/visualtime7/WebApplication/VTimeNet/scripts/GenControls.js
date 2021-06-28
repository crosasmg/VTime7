//--------------------------------------------------------------------
//- $$Workfile:: GenControls.js            $ 
//- $$Author:: Nvaplat53                    $ 
//- $$Date:: 3/09/04 13:19                  $ 
//- $$Revision:: 21                         $ 
//--------------------------------------------------------------------

//% genClientControl: Asigna a un objeto los atributos del control de clientes
//--------------------------------------------------------------------
function genClientControl(objControl) {
//--------------------------------------------------------------------

//alert('genClientControl2:' + objControl.value);

	objControl.docValue = '';
	objControl.codValue = '';
	objControl.docValueLen = 2;

    //-----------------------------------------------------------------------    
    function _regenValue() {
    //-----------------------------------------------------------------------    
        if (objControl.docValue!='' && objControl.codValue!='')
            objControl.value = objControl.docValue + objControl.codValue;
        else
            objControl.value = '';
//alert('regenValue:' + objControl.value);        
    }

    //-----------------------------------------------------------------------    
    function _getDocValue() {
    //-----------------------------------------------------------------------    
        var v_sret = objControl.value.substr(0, objControl.docValueLen);
        
//alert('getDocValue:' + v_sret);
        return v_sret;
    }

    //-----------------------------------------------------------------------    
    function _getCodValue() {
    //-----------------------------------------------------------------------    
        var v_sret = objControl.value.substr(objControl.docValueLen, objControl.value.length - 1);
//alert('getCodValue:'+v_sret);
        return v_sret;
    }

	//%setCode: Se asigna los valores al tipo de documento
	//-----------------------------------------------------------------------    
	function _setDocValue(strDoc) {
	//-----------------------------------------------------------------------    
		objControl.docValue = padLeft(strDoc, '0', objControl.docValueLen);
	    _regenValue();
	    
//alert('setDocValue:' + objControl.value);
	    
	}
	//%setCode: Se asigna los valores al codigo del documento
	//-----------------------------------------------------------------------    
	function _setCodValue(strCode) {
	//-----------------------------------------------------------------------    
	    if (strCode!='')
		    objControl.codValue = padLeft(strCode, '0', 14 - objControl.docValueLen);
        else
            objControl.codValue = '';
	    _regenValue();

//alert('setCodValue:' + objControl.value);

	}

    //% _distValues : Distribuye el valor del campo en los controles asociados
	//-----------------------------------------------------------------------    
	function _distValues(){
	//-----------------------------------------------------------------------    
	    objControl.form.elements[objControl.name + '_Doc'].value = Number(objControl.getDoc()) + '';
	    objControl.form.elements[objControl.name + '_Code'].value = objControl.getCode();
	    
	}

    //% _setValues: asigna valores a propiedades
	//-----------------------------------------------------------------------    
	function _setValues(){
	//-----------------------------------------------------------------------    

    //+Se obtiene valor actual del código    
        var v_str_code = objControl.getCode();
        var v_str_doc  = objControl.getDoc();
        
        //if ((v_str_code!='') || (v_str_doc!='')) {
        objControl.setDoc(v_str_doc);
        objControl.setCode(v_str_code);	
	    _distValues();
	    //}
	}


//+Se asignan funciones	para manejar codigos
	objControl.setDoc       = _setDocValue;
	objControl.setCode      = _setCodValue;
	objControl.getDoc       = _getDocValue;
	objControl.getCode      = _getCodValue;
	objControl.setValues    = _setValues;
	//objControl.setRelValues = _distValues;
	
//+Se cargan valores iniciales
    objControl.setValues();
    
}
