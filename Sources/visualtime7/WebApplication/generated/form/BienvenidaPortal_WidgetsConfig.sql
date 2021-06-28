SET DEFINE OFF;
DECLARE
   TYPE roleNameType IS VARRAY(3) OF VARCHAR2(255);
   TYPE roleIdType IS VARRAY(3) OF NUMBER(9);
   roleNameList roleNameType;
   total integer;
   
   Role_ID ROLE.ROLEID%TYPE;
   Widget_ID   Widget.ID%TYPE;
   WidgetsInRoles_ID WidgetsInRoles.ID%TYPE;
   RecordCount NUMBER(5);
BEGIN

   BEGIN
      SELECT ID
        INTO Widget_ID
        FROM Widget
       WHERE ModelId = '81bd858a-7828-4033-a297-6624763f2515';
   EXCEPTION
      WHEN NO_DATA_FOUND
      THEN
         Widget_ID := 0;
   END;
   DBMS_OUTPUT.PUT_LINE (' Widget Id actual: ' || Widget_ID);

   IF Widget_ID = 0
   THEN
      SELECT NVL (MAX (ID), 0) + 1
        INTO Widget_ID
        FROM Widget;
      INSERT INTO Widget (ID, 
                          Name, 
                          Url, 
                          Description, 
                          CreatedDate, 
                          LastUpdate, 
                          VersionNo, 
                          IsDefault, 
                          defaultState, 
                          Icon, 
                          OrderNo, 
                          IsAnonymouAllow, 
                          ModelId) 
                  VALUES (Widget_ID, 
                          'BienvenidaPortal', 
                          '/generated/form/BienvenidaPortalUserControl.ascx', 
                          'Bienvenida al portal', 
                           TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS'), 
                           TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS'), 
                          1, 
                          0, 
                          '&lt;state&gt;&lt;ModelId&gt;81bd858a-7828-4033-a297-6624763f2515&lt;/ModelId&gt;&lt;Release&gt;1&lt;/Release&gt;&lt;/state&gt;', 
                          '~\images\Dropthings\Form.png', 
                          1, 
                          0, 
                          '81bd858a-7828-4033-a297-6624763f2515'); 
        DBMS_OUTPUT.PUT_LINE (' Se crea el widget con el Id: ' || Widget_ID);

   ELSE
      UPDATE Widget 
         SET Name = 'BienvenidaPortal', 
             Description = 'Bienvenida al portal', 
             Url = '/generated/form/BienvenidaPortalUserControl.ascx', 
             LastUpdate = TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS'), 
             IsDefault = 0, 
             IsAnonymouAllow = 1, 
             DefaultState = '&lt;state&gt;&lt;ModelId&gt;81bd858a-7828-4033-a297-6624763f2515&lt;/ModelId&gt;&lt;Release&gt;1&lt;/Release&gt;&lt;/state&gt;' 
      WHERE ID = Widget_ID; 
        DBMS_OUTPUT.PUT_LINE (' Se actualiza el widget ');

   END IF;
BEGIN
      SELECT COUNT(ID)
        INTO RecordCount
        FROM WidgetTrans
       WHERE ID = Widget_ID AND LanguageID = 1;
   EXCEPTION
      WHEN NO_DATA_FOUND
      THEN
         RecordCount := 0;
   END;      
   
   IF RecordCount = 0
   THEN

      INSERT INTO WidgetTrans (ID, 
                               LanguageID, 
                               Name, 
                               Description, 
                               CreatorUserCode, 
                               CreationDate, 
                               UpdateUserCode, 
                               UpdateDate) 
                       VALUES (Widget_ID, 
                               1, 
                               'Welcome', 
                               'Bienvenida al portal', 
                               '1499', 
                               TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS'), 
                               '1499', 
                               TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS')); 

      DBMS_OUTPUT.PUT_LINE (' Se crea la descripción del widget para el lenguaje English ');
   ELSE
      UPDATE WidgetTrans 
         SET Name = 'Welcome', 
             Description = 'Bienvenida al portal', 
             UpdateDate = TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS') 
      WHERE ID = Widget_ID AND LanguageID = 1; 
      DBMS_OUTPUT.PUT_LINE (' Se actualiza la descripción del widget para el lenguaje English ');
   END IF;
BEGIN
      SELECT COUNT(ID)
        INTO RecordCount
        FROM WidgetTrans
       WHERE ID = Widget_ID AND LanguageID = 2;
   EXCEPTION
      WHEN NO_DATA_FOUND
      THEN
         RecordCount := 0;
   END;      
   
   IF RecordCount = 0
   THEN

      INSERT INTO WidgetTrans (ID, 
                               LanguageID, 
                               Name, 
                               Description, 
                               CreatorUserCode, 
                               CreationDate, 
                               UpdateUserCode, 
                               UpdateDate) 
                       VALUES (Widget_ID, 
                               2, 
                               'Bienvenido', 
                               'Bienvenida al portal', 
                               '1499', 
                               TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS'), 
                               '1499', 
                               TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS')); 

      DBMS_OUTPUT.PUT_LINE (' Se crea la descripción del widget para el lenguaje Spanish ');
   ELSE
      UPDATE WidgetTrans 
         SET Name = 'Bienvenido', 
             Description = 'Bienvenida al portal', 
             UpdateDate = TO_DATE('07/05/2019 11:09:02', 'MM/DD/YYYY HH24:MI:SS') 
      WHERE ID = Widget_ID AND LanguageID = 2; 
      DBMS_OUTPUT.PUT_LINE (' Se actualiza la descripción del widget para el lenguaje Spanish ');
   END IF;
    roleNameList := roleNameType('All');
    total := roleNameList.count;  

    DELETE FROM WidgetsInRoles WHERE WidgetId = Widget_ID;
    DBMS_OUTPUT.PUT_LINE ('Se eliminó los registros del widget en la tabla WidgetsInRoles con el id:' || Widget_ID);
    
    FOR i in 1 .. total LOOP
        DBMS_OUTPUT.PUT_LINE ('Buscando el rol:' || roleNameList(i));
        BEGIN
            SELECT ROLEID
              INTO Role_ID
              FROM ROLE
             WHERE ROLENAMELOW = LOWER(roleNameList(i));
        EXCEPTION
            WHEN NO_DATA_FOUND
            THEN
                Role_ID := 0;
        END;
            
        IF Role_ID = 0
        THEN
            SELECT NVL (MAX (ROLEID), 0) + 1
              INTO Role_ID
              FROM ROLE;        
            DBMS_OUTPUT.PUT_LINE ('Crear con id:' || Role_ID);
	    
      INSERT INTO ROLE (ROLEID, 
                        ROLENAME, 
                        ISBACKOFFICESOURCE, 
                        SECURITYLEVEL, 
                        ROLENAMELOW) 
                VALUES (Role_ID, 
                        roleNameList(i), 
                        0, 
                        9, 
                        LOWER(roleNameList(i))); 
        DBMS_OUTPUT.PUT_LINE (' Se crea el rol: ' || roleNameList(i));

        END IF;
          
       SELECT NVL (MAX (ID), 0) + 1
                   INTO WidgetsInRoles_ID
                  FROM WidgetsInRoles;        
       DBMS_OUTPUT.PUT_LINE ('Crear id para la tabla WidgetsInRoles:' || WidgetsInRoles_ID);
	     
      INSERT INTO WidgetsInRoles (ID, 
                                  WIDGETID, 
                                  ROLEID, 
                                  ISDEFAULT, 
                                  ISEDITALLOW, 
                                  ISALLOWEDTOEDITTHETITLE) 
                          VALUES (WidgetsInRoles_ID, 
                                  Widget_ID, 
                                  Role_ID, 
                                  0, 
                                  0, 
                                  0); 
      DBMS_OUTPUT.PUT_LINE (' Se crea en la tabla WidgetsInRoles el rol: ' || roleNameList(i));
	         
    END LOOP;
END;
