<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoRendicion.aspx.cs" Inherits="CRM.DemoRendicion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Rendicion Demo</title>
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/plugins/jquery-datatable/dataTables.bootstrap4.min.css">
    <link rel="stylesheet" href="assets/css/main1.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous">
    <style>
        .headerTable {
            background-color: black;
            color: white;
        }
    </style>
</head>
<body class="theme-purple">
    <form id="form1" runat="server">
        <div class="container">
            <asp:ScriptManager ID="ScriptManager2" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel runat="server" ID="UP">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="Panel1">
                        <div class="row ">
                            <div class="col-sm-12 text-center">
                                <h4>Rendición Web</h4>
                                <hr />
                                <h6>Ingrese Nº de Viaje para cargar la información del viaje</h6>
                                <div class="input-group mb-3">
                                    <asp:TextBox runat="server" ID="tNumOc" CssClass="form-control font-22"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" CssClass="btn btn-primary" OnClick="Unnamed_Click"><i class="fa fa-search fa-2x"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Panel2" Visible="false">
                        <div class="row">
                            <div class="col-sm-12">
                                <br />
                                <table class="table table-sm">
                                    <tr>
                                        <td>Conductor:</td>
                                        <td><b>Conductor Test 1</b></td>
                                    </tr>
                                    <tr>
                                        <td>GT Nº:</td>
                                        <td><b class="text-purple" style="font-size: large">10</b></td>
                                    </tr>
                                </table>
                                <br />
                                <h6><b>Rendición Combustible</b></h6>
                                <table class="table table-sm">
                                    <tr class="headerTable">
                                        <th>Fecha/Hora</th>
                                        <th>Estación</th>
                                        <th>Litros</th>
                                        <th>$</th>
                                        <th><i class="fa fa-edit"></i></th>
                                    </tr>
                                    <tr>
                                        <td>01/01/2021 17:30 Hrs</td>
                                        <td>CHAÑARAL</td>
                                        <td>407</td>
                                        <td>$207.507</td>
                                        <td><i class="fa fa-edit"></i></td>
                                    </tr>
                                    <tr>
                                        <td>01/01/2021 17:30 Hrs</td>
                                        <td>CHAÑARAL</td>
                                        <td>407</td>
                                        <td>$207.507</td>
                                        <td><i class="fa fa-edit"></i></td>
                                    </tr>
                                </table>
                                <a href="#modalcombustible" data-toggle="modal" data-target="#modalcombustible" class="btn btn-primary"><i class="fa fa-plus"></i>Agregar</a>
                                <hr />
                                <h6><b>Rendición Gastos</b></h6>
                                <table class="table table-sm">
                                    <tr class="headerTable">
                                        <th>Fecha/Hora</th>
                                        <th>Tipo</th>
                                        <th>Detalle</th>
                                        <th>$</th>
                                        <th><i class="fa fa-edit"></i></th>
                                        k
                                                      <th><i class="fa fa-edit"></i></th>
                                        k
                                    </tr>
                                    <tr>
                                        <td>01/01/2021 17:30 Hrs</td>
                                        <td>Peajes</td>
                                        <td></td>
                                        <td>$200.000</td>
                                        <td><i class="fa fa-edit"></i></td>
                                    </tr>
                                    <tr>
                                        <td>01/01/2021 17:30 Hrs</td>
                                        <td>Estacionamiento</td>
                                        <td>Coquimbo</td>
                                        <td>$10.000</td>
                                        <td><i class="fa fa-edit"></i></td>
                                    </tr>
                                </table>
                                <a href="#modalcombustible" data-toggle="modal" data-target="#modalcombustible" class="btn btn-primary"><i class="fa fa-plus"></i>Agregar</a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton runat="server" ID="B_FIN" OnClick="Unnamed_Click" CssClass="btn btn-primary">Cerrar</asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="modal fade" id="modalcombustible" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="title text-purple">Carga de Combustible</h4>
                    </div>
                    <hr />
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <small>ESTACION</small>
                                <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control font-22"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <small>LITROS</small>
                                <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control font-22"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <small>VALOR $</small>
                                <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control font-22"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

