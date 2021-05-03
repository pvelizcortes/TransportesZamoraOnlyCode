<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Requerimientos.aspx.cs" Inherits="AMALIA.Requerimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
    
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_NUEVO" UpdateMode="Conditional">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <span style="font-size: large"><strong>REQUERIMIENTOS</strong></span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                                         
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Requerimientos</li>
                                            </ul>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" EventName="RowCommand" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="body table-responsive">
                                                    <div style="display: none">
                                                        <a id="MODAL_GASTO" href="#modalgasto" data-toggle="modal" data-target="#modalgasto"></a>
                                                    </div>
                                                    <asp:GridView DataKeyNames="ID_REQUERIMIENTO, NOMBREPRIORIDAD, ESTADO, ID_OT" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_PRINCIPAL_RowCommand" OnRowDataBound="G_PRINCIPAL_RowDataBound" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="fa fa-eye fa-2x"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Nª REQUERIMIENTO" DataField="ID_REQUERIMIENTO" />
                                                            <asp:BoundField HeaderText="ID_OT" DataField="ID_OT" Visible="false" />
                                                            <asp:BoundField HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy}" DataField="FECHA" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="PRIORIDAD" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_prioridad" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_estado" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="CAMION" DataField="PATENTE" />
                                                            <asp:BoundField HeaderText="RAMPLA" DataField="PATENTE_RAMPLA" />
                                                            <asp:BoundField HeaderText="DESCRIPCION" DataField="DESCRIPCION" />
                                                            <asp:BoundField HeaderText="RESOLUCION" DataField="RESOLUCION" />
                                                            <asp:BoundField HeaderText="SOLICITANTE" DataField="NOMBRE_COMPLETO" />
                                                            <asp:BoundField HeaderText="PRIORIDAD" DataField="NOMBREPRIORIDAD" Visible="false" />
                                                            <asp:BoundField HeaderText="ESTADO" DataField="ESTADO" Visible="false" />

                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- .MODAL REQUERIMIENTOS. -->
    <div class="modal fade" id="modalgasto" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel runat="server" ID="UP_GASTO_GENERAL" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="B_GENERAOT" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="B_RECHAZAR" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="B_BORRAR" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="body widget-user">
                                            <div style="display: none">
                                                <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                            </div>
                                            <div class="row">
                                                <div class="col-3 text-center">
                                                    <div id="DIV_PRIORIDAD_ICON" runat="server"></div>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_PRIORIDAD"></span></h5>
                                                    <small>PRIORIDAD</small>
                                                </div>
                                                <div class="col-3 text-center">
                                                    <i class="fa fa-calendar col-deep-purple fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_FECHA"></span></h5>
                                                    <small>FECHA SOLICITUD</small>
                                                </div>
                                                <div class="col-3 text-center">
                                                    <i class="fa fa-truck col-amber fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_CAMION"></span></h5>
                                                    <small>CAMIÓN</small>
                                                </div>
                                                <div class="col-3 text-center">
                                                    <i class="fa fa-pallet col-amber fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_RAMPLA"></span></h5>
                                                    <small>RAMPLA</small>
                                                </div>
                                            </div>
                                            <hr>
                                            <img class="rounded-circle" src="assets/images/profile_av.jpg" alt="">
                                            <div class="wid-u-info">
                                                <h5>Solicita: <span runat="server" id="DIV_NOMBRE_CONDUCTOR"></span></h5>
                                                <p class="text-muted m-b-0">
                                                    <b>Requerimiento:</b><br />
                                                    <span runat="server" id="DIV_OBSERVACION"></span>
                                                </p>
                                            </div>
                                            <hr />
                                            <h4 class="text-center">Ingrese una observación y luego seleccione su resolución.</h4>
                                            <div class="row clearfix">
                                                <div class="col-12 text-center">
                                                    <asp:TextBox runat="server" ID="T_RESOLUCION" TextMode="MultiLine" Rows="4" CssClass="form-control border" placeholder="Ingrese su observación aqui y seleccione una opcion..."></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row clearfix">
                                                <div class="col-4 text-center">
                                                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-simple btn-block" ID="B_GENERAOT" ClientIDMode="AutoID" OnClick="B_GENERAOT_Click" OnClientClick="relojito(true);">
                                                       <i class="fa fa-calendar-check fa-2x"></i><br /> GENERAR OT
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-4 text-center">
                                                    <asp:LinkButton runat="server" CssClass="btn btn-warning btn-simple btn-block" ID="B_RECHAZAR" ClientIDMode="AutoID" OnClick="B_RECHAZAR_Click" OnClientClick="relojito(true);">
                                                       <i class="fa fa-clock fa-2x"></i><br /> EN ESPERA
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-4 text-center">
                                                    <asp:LinkButton runat="server" CssClass="btn btn-danger btn-simple btn-block" ID="B_BORRAR" ClientIDMode="AutoID" OnClick="B_BORRAR_Click" OnClientClick="relojito(true);">
                                                       <i class="fa fa-times-circle fa-2x"></i><br /> RECHAZAR
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" id="CERRAR_MODAL" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });

        function Datatables() {
            $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'lBfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                },
                columnDefs: [{
                    type: 'date-uk',
                    targets: [2]
                }]   
            });
        }

        function ComboPro() {
            $('.combopro').selectize();
        }

        function GASTOGENERAL() {
            $('#MODAL_GASTO').click();
        }

        function cerramodal() {
            $('#CERRAR_MODAL').click();
        }
    </script>
</asp:Content>
