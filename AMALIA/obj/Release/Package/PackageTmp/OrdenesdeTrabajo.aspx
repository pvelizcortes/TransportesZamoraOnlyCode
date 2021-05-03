<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="OrdenesdeTrabajo.aspx.cs" Inherits="AMALIA.OrdenesdeTrabajo" %>

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
                                            <span style="font-size: large"><strong>ORDEN DE TRABAJO</strong></span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                                         
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Orden de Trabajo</li>
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
                                                    <asp:GridView DataKeyNames="id_ot, id_estado_ot, nombre_estado" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_PRINCIPAL_RowCommand" OnRowDataBound="G_PRINCIPAL_RowDataBound" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" OnClientClick="relojito(true);">
                                                                   <i class="fa fa-eye fa-2x"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="OT Nº" DataField="id_ot" />
                                                            <asp:BoundField HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy}" DataField="fecha_creacion" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ESTADO" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_estado" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="CAMION" DataField="PATENTE" />
                                                            <asp:BoundField HeaderText="RAMPLA" DataField="PATENTE_RAMPLA" />
                                                            <asp:BoundField HeaderText="REP. INTERNA $" DataField="suma_rep_interna" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="REP. EXTERNAS $" DataField="suma_rep_externa" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="REP. TOTAL $" DataField="suma_rep_total" DataFormatString="{0:C0}" />
                                                            <asp:BoundField HeaderText="SOLICITANTE" DataField="nombre_Completo" />
                                                            <asp:BoundField HeaderText="ESTADO" DataField="id_estado_ot" Visible="false" />
                                                            <asp:BoundField HeaderText="ESTADO" DataField="nombre_estado" Visible="false" />
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
                    <asp:AsyncPostBackTrigger ControlID="G_LOG_OT" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="G_REPUESTOS" EventName="RowCommand" />
                </Triggers>
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="row clearfix">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="body widget-user">
                                            <div style="display: none">
                                                <asp:TextBox runat="server" ID="T_ID" ClientIDMode="Static"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="ESTADO_I"></asp:TextBox>
                                            </div>
                                            <div class="row">
                                                <div class="col-1 text-center">
                                                    <i class="fa fa-file col-deep-purple fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_NUM_OT"></span></h5>
                                                    <small>NUM OT</small>
                                                </div>
                                                <div class="col-2 text-center">
                                                    <div id="DIV_PRIORIDAD_ICON" runat="server"></div>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_PRIORIDAD"></span></h5>
                                                    <small>PRIORIDAD</small>
                                                </div>
                                                <div class="col-2 text-center">
                                                    <i class="fa fa-calendar col-deep-purple fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_FECHA"></span></h5>
                                                    <small>FECHA CREACION OT</small>
                                                </div>
                                                <div class="col-2 text-center">
                                                    <i class="fa fa-truck col-amber fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_CAMION"></span></h5>
                                                    <small>CAMIÓN</small>
                                                </div>
                                                <div class="col-2 text-center">
                                                    <i class="fa fa-pallet col-amber fa-2x"></i>
                                                    <h5 class="m-b-0"><span runat="server" id="DIV_RAMPLA"></span></h5>
                                                    <small>RAMPLA</small>
                                                </div>
                                                <div class="col-3">
                                                    <img class="rounded-circle" src="assets/images/profile_av.jpg" alt="">
                                                    <div class="wid-u-info">
                                                        <h5><span runat="server" id="DIV_NOMBRE_CONDUCTOR"></span></h5>
                                                        <p class="text-muted m-b-0">
                                                            <span runat="server" id="DIV_OBSERVACION"></span>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <h4><b class="text-purple"><i class="fa fa-list"></i>&nbsp;HISTORIAL</b> DE LA ORDEN DE TRABAJO</h4>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <b>OBSERVACION:</b>
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" ID="T_OBSERVACION" CssClass="form-control" placeholder="Agregue una observación..."></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="B_AGREGAR_COMENTARIO" runat="server" OnClick="B_AGREGAR_COMENTARIO_Click" ClientIDMode="AutoID" CssClass="btn-block btn btn-success" OnClientClick="relojito(true);">AGREGAR</i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="body table-responsive">
                                                        <asp:GridView ClientIDMode="AutoID" runat="server" ID="G_LOG_OT" CssClass="table table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="FECHA" DataField="fecha" />
                                                                <asp:BoundField HeaderText="USUARIO" DataField="usuario" />
                                                                <asp:BoundField HeaderText="DEL ESTADO" DataField="nom_estado_i" />
                                                                <asp:BoundField HeaderText="AL ESTADO" DataField="nom_estado_f" />
                                                                <asp:BoundField HeaderText="OBSERVACION" DataField="observacion" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No se encontraron resultados.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <h4><b class="text-purple"><i class="fa fa-cogs"></i>&nbsp;REPUESTOS</b> UTILIZADOS EN LA ORDEN DE TRABAJO</h4>
                                            <div class="row">
                                                <div class="col-2">
                                                    <small><b>CATEGORIA</b></small>
                                                    <asp:DropDownList runat="server" ID="CB_CATEGORIA" AutoPostBack="true" OnSelectedIndexChanged="CB_CATEGORIA_SelectedIndexChanged" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                                <div class="col-2">
                                                    <small><b>MARCA</b></small>
                                                    <asp:DropDownList runat="server" ID="CB_MARCA" AutoPostBack="true" OnSelectedIndexChanged="CB_MARCA_SelectedIndexChanged" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                    <small><b>REPUESTOS</b></small>
                                                    <asp:DropDownList runat="server" ID="CB_REPUESTO" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                                <div class="col-1">
                                                    <small><b>CANTIDAD</b></small>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="T_CANTIDAD_REPUESTOS" Text="1" TextMode="Number"></asp:TextBox>
                                                </div>
                                                <div class="col-2">
                                                    <small><b>VALOR TOTAL</b></small>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="T_VALOR_REPUESTOS" Text="0" TextMode="Number"></asp:TextBox>
                                                </div>
                                                <div class="col-2">
                                                    <br />
                                                    <asp:LinkButton ID="B_AGREGAR_REPUESTO" runat="server" OnClick="B_AGREGAR_REPUESTO_Click" ClientIDMode="AutoID" CssClass="btn btn-success btn-block" OnClientClick="relojito(true);">AGREGAR</i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="body table-responsive">
                                                        <asp:GridView DataKeyNames="id_ot_producto, nom_producto, id_producto, cantidad" ClientIDMode="AutoID" runat="server" ID="G_REPUESTOS" CssClass="table table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="G_REPUESTOS_RowCommand">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="id" DataField="id_ot_producto" Visible="false" />
                                                                <asp:BoundField HeaderText="id_producto" DataField="id_producto" Visible="false" />
                                                                <asp:BoundField HeaderText="CATEGORIA" DataField="nombre_categoria" />
                                                                <asp:BoundField HeaderText="MARCA" DataField="nombre_marca" />
                                                                <asp:BoundField HeaderText="PRODUCTO" DataField="nom_producto" />
                                                                <asp:BoundField HeaderText="CANTIDAD" DataField="cantidad" />
                                                                <asp:BoundField HeaderText="VALOR" DataField="valor" DataFormatString="{0:C0}" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar">
                                                                   <i class="fa fa-times fa-2x"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No se encontraron resultados.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <h4><b class="text-purple"><i class="fa fa-toolbox"></i>&nbsp;REPARACIONES</b> EXTERNAS</h4>
                                            <div class="row">
                                                <div class="col-3">
                                                    <small><b>PROVEEDOR</b></small>
                                                    <asp:DropDownList runat="server" ID="CB_REPEX_PROVEEDOR" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                    <small><b>FACTURA</b></small>
                                                    <asp:TextBox runat="server" ID="T_REPEX_FACTURA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-3">
                                                    <small><b>VALOR</b></small>
                                                    <asp:TextBox runat="server" TextMode="Number" ID="T_REPEX_VALOR" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-3">
                                                    <br />
                                                    <asp:LinkButton ID="B_AGREGAR_REPEX" runat="server" OnClick="B_AGREGAR_REPEX_Click" ClientIDMode="AutoID" CssClass="btn btn-success btn-block" OnClientClick="relojito(true);">AGREGAR</i></asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="body table-responsive">
                                                        <asp:GridView DataKeyNames="id_ot_repex, id_ot" ClientIDMode="AutoID" runat="server" ID="G_REPEX" CssClass="table table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="G_REPEX_RowCommand">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="id_ot_repex" DataField="id_ot_repex" Visible="false" />
                                                                <asp:BoundField HeaderText="id_ot" DataField="id_ot" Visible="false" />
                                                                <asp:BoundField HeaderText="PROVEEDOR" DataField="nom_proveedor" />
                                                                <asp:BoundField HeaderText="Nº FACTURA" DataField="num_factura" />
                                                                <asp:BoundField HeaderText="VALOR" DataField="valor" DataFormatString="{0:C0}" />
                                                                <asp:BoundField HeaderText="FECHA" DataField="fecha" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar">
                                                                   <i class="fa fa-times fa-2x"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No se encontraron resultados.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <h4><b class="text-purple"><i class="fa fa-sync"></i>&nbsp;CAMBIAR ESTADO</b> DE LA ORDEN DE TRABAJO</h4>
                                            <div class="row">
                                                <div class="col-9">
                                                    <asp:DropDownList runat="server" ID="CB_ESTADO_OT" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                                <div class="col-3">
                                                    <asp:LinkButton ID="B_GUARDAR_MODAL" runat="server" OnClick="B_GUARDAR_MODAL_Click" ClientIDMode="AutoID" CssClass="btn btn-success btn-block">CAMBIAR ESTADO</asp:LinkButton>
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
            var x = $('#T_ID').val();
            if (x != '') {
                GASTOGENERAL();
            }
        });

        function Datatables() {
            try {
                $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                    destroy: true,
                    stateSave: true,
                    dom: 'lBfrtip',
                    buttons: [
                        'copy', 'print'
                    ],
                    "language": {
                        "url": "assets/Spanish.json"
                    }
                });
            }
            catch (ex) {

            }
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
