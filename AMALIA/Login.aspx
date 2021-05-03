<%@ Page Language="C#" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AMALIA.Login" %>

<!doctype html>
<html class="no-js " lang="es">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <meta name="description" content="CLoud Mant, Mantenimiento en la Nube.">

    <title>TRANSPORTES ZAMORA | INGRESO </title>
    <!-- Favicon-->
    <link rel="icon" href="favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css">

    <!-- Custom Css -->
    <link rel="stylesheet" href="assets/css/main1.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
</head>
<body class="theme-purple">
    <form runat="server">    
        <div class="authentication" style="background-image: url(assets/images/fondologin.jpg); background-position: center; background-size: cover">
            <div class="container">
                <div class="col-md-12 content-center">
                    <div class="row clearfix">
                        <div class="col-sm-6 offset-sm-3">
                            <div class="company_detail">
                                <h4 class="logo">
                                    <img src="assets/images/logo.svg" alt="Logo">
                                    TRANSPORTES ZAMORA | ERP </h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6  offset-sm-3">
                            <div class="card-plain">
                                <div class="header">
                                    <h5>Ingresar</h5>
                                </div>
                                <div class="form">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="T_USUARIO" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                                        <span class="input-group-addon"><i class="zmdi zmdi-account-circle"></i></span>
                                    </div>
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="T_PASS" CssClass="form-control" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                        <span class="input-group-addon"><i class="zmdi zmdi-lock"></i></span>
                                    </div>
                                    <div class="input-group">
                                        <asp:Label runat="server" ID="LBL_RESPUESTA" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="footer">
                                    <asp:Button runat="server" ID="B_INGRESAR" CssClass="btn btn-primary btn-round btn-block" OnClick="B_INGRESAR_Click" Text="Ingresar" />
                                    <%--  <a href="Registro.aspx" class="btn btn-primary btn-simple btn-round btn-block">Registrarse</a>--%>
                                </div>                             
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>      
    </form>

    <!-- Jquery Core Js -->
    <script src="assets/bundles/libscripts.bundle.js"></script>
    <script src="assets/bundles/vendorscripts.bundle.js"></script>
    <!-- Lib Scripts Plugin Js -->

    <script src="assets/plugins/particles-js/particles.min.js"></script>
    <script src="assets/plugins/particles-js/particles.js"></script>
</body>
</html>
