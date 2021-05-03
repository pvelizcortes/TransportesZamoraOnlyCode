<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/AmaliaGD.Master" AutoEventWireup="true" CodeBehind="GD_NEUMATICOS.aspx.cs" Inherits="AMALIA.GD_NEUMATICOS" %>

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
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <span style="font-size: large"><strong>NEUMATICOS</strong> </span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="B_NUEVO" CssClass="btn btn-primary btn-sm" OnClick="B_NUEVO_Click"><i class="fa fa-dot-circle"></i>&nbsp;<i class="fa fa-exchange-alt"></i>&nbsp;INGRESAR CAMBIO NEUMATICO</asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="gd_dashboard.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="gd_dashboard.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Neumaticos</li>
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
                                            <asp:AsyncPostBackTrigger ControlID="B_GUARDAR" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="B_VOLVER" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="body table-responsive">
                                                    <asp:GridView DataKeyNames="ID_NEUMATICO" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered table-striped table-hover table-sm table-condensed" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ID_NEUMATICO" DataField="ID_NEUMATICO" Visible="false" />
                                                            <asp:BoundField HeaderText="Lugar" DataField="lugar_neumatico" />
                                                            <asp:BoundField HeaderText="Num. de Fuego" DataField="num_interno" />
                                                            <asp:BoundField HeaderText="Marca" DataField="marca" />
                                                            <asp:BoundField HeaderText="Posición" DataField="posicion" />
                                                            <asp:BoundField HeaderText="Patente" DataField="patente" />
                                                            <asp:BoundField HeaderText="Motivo" DataField="motivo_cambio" />
                                                            <asp:BoundField HeaderText="Fecha Ingreso" DataField="fecha_ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField HeaderText="Km Ingreso" DataField="km_ingreso" />
                                                            <asp:BoundField HeaderText="presion" DataField="presion" />
                                                            <asp:BoundField HeaderText="prof. izq" DataField="prof_izq" />
                                                            <asp:BoundField HeaderText="prof. der" DataField="prof_der" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                   <i class="icon-trash"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="PANEL_DETALLE1" Visible="false">
                                                <div class="body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <h2><strong>INFORMACION</strong> DEL NEUMATICO</h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Seleccione:</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:RadioButton Font-Size="Medium" runat="server" GroupName="activo" ID="RB_TRACTO" Text="Tracto" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            <asp:RadioButton Font-Size="Medium" runat="server" GroupName="activo" ID="RB_SEMI" Text="Semi" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Nº de Fuego</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_NUM_INTERNO" TextMode="Number" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Marca</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList runat="server" ID="CB_MARCA" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Posición</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList runat="server" ID="CB_POSICION" CssClass="form-control">
                                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Camión</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_CAMION" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Motivo Cambio</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_MOTIVO_CAMBIO" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="Neumáticos Reventados" Value="Neumáticos Reventados" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="Neumáticos Pinchados" Value="Neumáticos Pinchados"></asp:ListItem>
                                                                                <asp:ListItem Text="Neumáticos que presentan cortes y que comprometen todas las telas de la banda de rodado" Value="Neumáticos que presentan cortes y que comprometen todas las telas de la banda de rodado"></asp:ListItem>
                                                                                <asp:ListItem Text="Neumáticos con profundidad inferior a 4mm" Value="Neumáticos con profundidad inferior a 4mm"></asp:ListItem>                                                                                
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Fecha Ingreso</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_FECHA_INGRESO" TextMode="Date" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Km. Ingreso</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" TextMode="Number" ID="T_KM_INGRESO" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Presion</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_PRESION" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Profundidad Izq.</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_PROF_IZQ" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Profundidad Der.</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_PROF_DER" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                </div>
                                                            </form>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR</asp:LinkButton>
                                                    <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>
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
                }
            });
        }

    </script>
</asp:Content>
