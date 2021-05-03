<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Camion.aspx.cs" Inherits="AMALIA.Camion" %>

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
                                            <span style="font-size: large"><strong>CAMIONES</strong> </span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton ClientIDMode="AutoID" runat="server" ID="B_NUEVO" CssClass="btn btn-primary btn-sm" OnClick="B_NUEVO_Click"><i class="fa fa-truck"></i>&nbsp;CREAR NUEVO CAMION</asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Camiones</li>
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
                                                    <asp:GridView DataKeyNames="ID_CAMION, activo" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="ID_CAMION" DataField="ID_CAMION" Visible="false" />
                                                            <asp:BoundField HeaderText="Patente" DataField="patente" />
                                                              <asp:BoundField HeaderText="Num. Chasis" DataField="num_chasis" />
                                                              <asp:BoundField HeaderText="Num. Motor" DataField="num_motor" />
                                                            <asp:BoundField HeaderText="Marca" DataField="marca" />
                                                            <asp:BoundField HeaderText="Año" DataField="ano" />                                                            
                                                            <asp:BoundField HeaderText="Carga" DataField="carga" />                                                           
                                                            <asp:BoundField HeaderText="Estado" DataField="activo" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Cambiarestado">
                                                                   <i class="icon-refresh"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
                                                            <h2><strong>INFORMACION</strong> DEL CAMIÓN</h2>
                                                            <hr />
                                                            <form class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Patente</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_PATENTE" CssClass="form-control amalia-control" placeholder="Ej: ZN-1744"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Num. Chasis</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_NUM_CHASIS" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Num. Motor</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_NUM_MOTOR" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                 <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Num. Vin</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_VIN" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Marca</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList runat="server" ID="CB_MARCA"  CssClass="form-control amalia-control">
                                                                            </asp:DropDownList>                                                                            
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                 <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Modelo</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_MODELO" CssClass="form-control amalia-control" placeholder=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div> 
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Año</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" TextMode="Number" ID="T_ANO" CssClass="form-control amalia-control" placeholder="Ej: 2018"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>                                                                
                                                                <div class="row clearfix">
                                                                    <div class="col-lg-2 col-md-2 col-sm-4 form-control-label">
                                                                        <label>Carga Máxima</label>
                                                                    </div>
                                                                    <div class="col-lg-10 col-md-10 col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_CARGA" CssClass="form-control amalia-control" placeholder="Ej: 2000 kg"></asp:TextBox>
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
