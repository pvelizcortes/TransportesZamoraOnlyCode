<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imprimeChecklist.aspx.cs" Inherits="CRM.imprimeChecklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script>

        function Mensaje(mensaje) {
            alert(mensaje);
        }
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Enviando Correos </title>
    <script>
        $(document).ready(function () {
            Cierrame();
        });
        function Cierrame() {
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>Checklist finalizado con éxito, correos enviados (Puede cerrar esta ventana).</h4>
        </div>
    </form>
</body>

</html>
