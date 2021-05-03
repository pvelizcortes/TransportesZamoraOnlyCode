<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AUT_OC.aspx.cs" Inherits="CRM.AUT_OC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>AUTORIZAR OC</title>
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/plugins/jquery-datatable/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" href="assets/css/main1.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous">
</head>
<body class="theme-purple">

    <div class="container">

        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="UP">
                <ContentTemplate>
                    <div id="DIV1" runat="server">
                        <div style="display: none">
                            <asp:TextBox runat="server" ID="ID_DET"></asp:TextBox>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="text-center" style="width: 100%">
                                    <h4 runat="server" style="color: mediumpurple" id="LBL_ENCABEZADO"></h4>
                                </div>

                                <hr />
                                <h6>Encabezado</h6>
                                <table class="table table-sm">
                                    <tr>
                                        <td><b>Proveedor:</b></td>
                                        <td class="text-right">
                                            <asp:Label runat="server" ID="proveedor"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Solicitante:</b></td>
                                        <td class="text-right">
                                            <asp:Label runat="server" ID="solicitante"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Fecha OC:</b></td>
                                        <td class="text-right">
                                            <asp:Label runat="server" ID="fechaoc"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Plazo Entrega:</b></td>
                                        <td class="text-right">
                                            <asp:Label runat="server" ID="plazoentrega"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><b>Estado:</b></td>
                                        <td class="text-right">
                                            <asp:Label runat="server" ID="estado"></asp:Label></td>
                                    </tr>
                                </table>
                                <div runat="server" id="TABLADET" style="overflow-x: auto; width: 100%"></div>
                                <hr />
                                <div style="width: 100%" class="text-center">
                                    <h6>Archivos Adjuntos</h6>
                                    <div runat="server" id="TABLAADJUNTOS" style="overflow-x: auto; width: 100%"></div>
                                    <hr />
                                </div>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <table class="table">
                                    <tr>
                                       <%-- <th>Autorizada por Mauricio Zapata:
                                        </th>--%>
                                        <th>Autorizada por Francisca Estay:
                                        </th>
                                    </tr>
                                    <tr>
                                    <%--    <td>
                                            <b><span id="ESTADO_ACTUALMZ" runat="server" style="font-weight: bold"></span></b>
                                        </td>--%>
                                        <td>
                                            <b><span id="ESTADO_ACTUALFZ" runat="server" style="font-weight: bold"></span></b>
                                        </td>
                                    </tr>
                                </table>
                                <small>Desea agregar una observacion?</small><br />
                                <asp:TextBox runat="server" ID="T_MOTIVORECHAZO" Style="width: 100%; padding: 0px 10px 0px 10px;"></asp:TextBox><hr>
                                <small>Ingrese su clave de autorización</small>
                                <br />
                                <asp:TextBox runat="server" ID="T_CLAVE" TextMode="Number" CssClass="tz-input text-center" Font-Size="Large"></asp:TextBox><br />

                                <small id="RESPUESTA_CLAVE" runat="server" class="text-danger"></small>
                                <table class="table" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:LinkButton runat="server" ID="B_AUTORIZAR" CssClass="btn btn-success btn-lg" OnClick="B_AUTORIZAR_Click">AUTORIZAR <i class="fa fa-check"></i></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="B_RECHAZAR" CssClass="btn btn-danger btn-lg" OnClick="B_RECHAZAR_Click">RECHAZAR <i class="fa fa-times"></i></asp:LinkButton><br />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <br />
                                <br />

                            </div>
                        </div>
                    </div>
                    <div runat="server" id="DIV2" visible="false" class="text-center">
                        <h4 id="RESPUESTA" runat="server"></h4>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>


    </div>
    <%--</body>--%>
</html>
