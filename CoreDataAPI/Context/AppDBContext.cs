using CoreDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreDataAPI.Context;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }
    public virtual DbSet<Proyecto> Proyectos { get; set; }
    public virtual DbSet<Unidad> Unidades { get; set; }
    public virtual DbSet<Inventario> Inventarios { get; set; }
    public virtual DbSet<Elemento> Elementos { get; set; }
    public virtual DbSet<RelacionTipologiaUbicacion> RelacionTipologiaUbicaciones { get; set; }
    public virtual DbSet<RelacionUbicacionElemento> RelacionUbicacionElementos { get; set; }
    public virtual DbSet<UbicacionesUnidad> UbicacionesUnidades { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Detalle> Detalles { get; set; }
    public virtual DbSet<RequestInfoCategoria> RequestInfoCategorias { get; set; }
    public virtual DbSet<RequestInfoElemento> RequestInfoElementos { get; set; }
    public virtual DbSet<RequestInfoUbicacion> RequestInfoUbicaciones { get; set; }
    public virtual DbSet<RequestInfoUnidad> RequestInfoUnidades { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.SkProyecto).HasName("PK_TBL_DIM_PROYECTOS_sk_proyecto");

            entity.ToTable("TBL_DIM_PROYECTOS");

            entity.Property(e => e.SkProyecto)
                .ValueGeneratedNever()
                .HasColumnName("sk_proyecto");
            entity.Property(e => e.BanVis).HasColumnName("ban_vis");
            entity.Property(e => e.FecCargaDwh).HasColumnName("fec_carga_dwh");
            entity.Property(e => e.FecEntrega).HasColumnName("fec_entrega");
            entity.Property(e => e.FecFinalizacion).HasColumnName("fec_finalizacion");
            entity.Property(e => e.NkProyecto).HasColumnName("nk_proyecto");
            entity.Property(e => e.NumEstado).HasColumnName("num_estado");
            entity.Property(e => e.NumIdClaseProyecto).HasColumnName("num_id_clase_proyecto");
            entity.Property(e => e.NumIdProyecto).HasColumnName("num_id_proyecto");
            entity.Property(e => e.NumNaturaleza).HasColumnName("num_naturaleza");
            entity.Property(e => e.StrCiudad).HasColumnName("str_ciudad");
            entity.Property(e => e.StrDireccion).HasColumnName("str_direccion");
            entity.Property(e => e.StrEntFiduciaria).HasColumnName("str_ent_fiduciaria");
            entity.Property(e => e.StrEntidadCredito).HasColumnName("str_entidad_credito");
            entity.Property(e => e.StrEstrato).HasColumnName("str_estrato");
            entity.Property(e => e.StrEtapa).HasColumnName("str_etapa");
            entity.Property(e => e.StrEtapaProyecto).HasColumnName("str_etapa_proyecto");
            entity.Property(e => e.StrFiducia).HasColumnName("str_fiducia");
            entity.Property(e => e.StrInterior).HasColumnName("str_interior");
            entity.Property(e => e.StrNitEntCredito).HasColumnName("str_nit_ent_credito");
            entity.Property(e => e.StrNitEntFiduciaria).HasColumnName("str_nit_ent_fiduciaria");
            entity.Property(e => e.StrNombreClase).HasColumnName("str_nombre_clase");
            entity.Property(e => e.StrNombreEstado).HasColumnName("str_nombre_estado");
            entity.Property(e => e.StrNombreProyecto).HasColumnName("str_nombre_proyecto");
            entity.Property(e => e.StrTipoProyecto).HasColumnName("str_tipo_proyecto");
        });

        modelBuilder.Entity<Unidad>(entity =>
        {
            entity.HasKey(e => e.SkUnidad).HasName("PK_TBL_DIM_UNIDAD_sk_unidad");

            entity.ToTable("TBL_DIM_UNIDAD");

            entity.Property(e => e.SkUnidad)
                .ValueGeneratedNever()
                .HasColumnName("sk_unidad");
            entity.Property(e => e.FecCargaDwh).HasColumnName("fec_carga_dwh");
            entity.Property(e => e.NkUnidad).HasColumnName("nk_unidad");
            entity.Property(e => e.NumAreaConstruida).HasColumnName("num_area_construida");
            entity.Property(e => e.NumCodTipoInmueble).HasColumnName("num_cod_tipo_inmueble");
            entity.Property(e => e.StrAptUnidad).HasColumnName("str_apt_unidad");
            entity.Property(e => e.StrCodUnidad).HasColumnName("str_cod_unidad");
            entity.Property(e => e.StrEstadoUnidad).HasColumnName("str_estado_unidad");
            entity.Property(e => e.StrTipoInmueble).HasColumnName("str_tipo_inmueble");
            entity.Property(e => e.StrTipoUnidad).HasColumnName("str_tipo_unidad");
            entity.Property(e => e.StrTipolArea).HasColumnName("str_tipol_area");
            entity.Property(e => e.StrTipologia).HasColumnName("str_tipologia");
            entity.Property(e => e.ValUnidad).HasColumnName("val_unidad");
            entity.Property(e => e.ValUnidadListaVigente).HasColumnName("val_unidad_lista_vigente");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.SkInventario).HasName("PK_TBL_FACT_INVENTARIO_sk_inventario");

            entity.ToTable("TBL_FACT_INVENTARIOS");

            entity.Property(e => e.SkInventario)
                .ValueGeneratedNever()
                .HasColumnName("sk_inventario");
            entity.Property(e => e.FecCargaDwh).HasColumnName("fec_carga_dwh");
            entity.Property(e => e.FltAreaBalcon).HasColumnName("flt_area_balcon");
            entity.Property(e => e.FltAreaConstruida).HasColumnName("flt_area_construida");
            entity.Property(e => e.FltAreaJardineria).HasColumnName("flt_area_jardineria");
            entity.Property(e => e.FltAreaPatio).HasColumnName("flt_area_patio");
            entity.Property(e => e.FltAreaPrivada).HasColumnName("flt_area_privada");
            entity.Property(e => e.FltAreaTecnica).HasColumnName("flt_area_tecnica");
            entity.Property(e => e.FltAreaTerraza).HasColumnName("flt_area_terraza");
            entity.Property(e => e.FltAreaTotal).HasColumnName("flt_area_total");
            entity.Property(e => e.NumAlcobas).HasColumnName("num_alcobas");
            entity.Property(e => e.NumCodVenta).HasColumnName("num_cod_venta");
            entity.Property(e => e.NumKeyOrder).HasColumnName("num_key_order");
            entity.Property(e => e.NumMetaDinero).HasColumnName("num_meta_dinero");
            entity.Property(e => e.NumMetaUnidades).HasColumnName("num_meta_unidades");
            entity.Property(e => e.NumPiso).HasColumnName("num_piso");
            entity.Property(e => e.NumPromedioVent).HasColumnName("num_promedio_vent");
            entity.Property(e => e.NumTotalProyeccion).HasColumnName("num_total_proyeccion");
            entity.Property(e => e.SkCliente).HasColumnName("sk_cliente");
            entity.Property(e => e.SkEmpresa).HasColumnName("sk_empresa");
            entity.Property(e => e.SkMacroproyecto).HasColumnName("sk_macroproyecto");
            entity.Property(e => e.SkProyeccion).HasColumnName("sk_proyeccion");
            entity.Property(e => e.SkProyecto).HasColumnName("sk_proyecto");
            entity.Property(e => e.SkStatusInventario).HasColumnName("sk_status_inventario");
            entity.Property(e => e.SkTiempo).HasColumnName("sk_tiempo");
            entity.Property(e => e.SkUnidad).HasColumnName("sk_unidad");
            entity.Property(e => e.SkVenta).HasColumnName("sk_venta");
            entity.Property(e => e.ValUnidad).HasColumnName("val_unidad");

            entity.HasOne(d => d.SkProyectoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.SkProyecto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FactInventarios_DimProyectos");

            entity.HasOne(d => d.SkUnidadNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.SkUnidad)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FactInventarios_DimUnidad");
        });

        modelBuilder.Entity<Elemento>(entity =>
        {
            entity.HasKey(e => e.SkElemento).HasName("PK__TBL_ELEM__F9ED54FCF4AB7ED7");

            entity.ToTable("TBL_ELEMENTOS", "form");

            entity.Property(e => e.SkElemento).HasColumnName("sk_elemento");
            entity.Property(e => e.StrNombreElemento).HasColumnName("str_nombre_elemento");
        });

        modelBuilder.Entity<RelacionTipologiaUbicacion>(entity =>
        {
            entity.HasKey(e => e.SkRelation);

            entity.ToTable("TBL_RELACION_TIPOLOGIA_UBICACION", "form");

            entity.Property(e => e.SkRelation).HasColumnName("sk_relation");
            entity.Property(e => e.SkUbicacion).HasColumnName("sk_ubicacion");
            entity.Property(e => e.StrTipologia)
                .HasMaxLength(5)
                .HasColumnName("str_tipologia");

            entity.HasOne(d => d.SkUbicacionNavigation).WithMany(p => p.RelacionTipologiaUbicacions)
                .HasForeignKey(d => d.SkUbicacion)
                .HasConstraintName("FK_TBL_RELACION_TIPOLOGIA_UBICACION_TBL_UBICACIONES_UNIDAD");
        });

        modelBuilder.Entity<RelacionUbicacionElemento>(entity =>
        {
            entity.HasKey(e => e.SkRelation).HasName("PK__TBL_RELA__4AA6C04A7E3D7FAF");

            entity.ToTable("TBL_RELACION_UBICACION_ELEMENTOS", "form");

            entity.Property(e => e.SkRelation).HasColumnName("sk_relation");
            entity.Property(e => e.SkElemento).HasColumnName("sk_elemento");
            entity.Property(e => e.SkUbicacionesUnidad).HasColumnName("sk_ubicaciones_unidad");
            entity.Property(e => e.StrTipoEntrega)
                .HasMaxLength(255)
                .HasColumnName("str_tipo_entrega");
            entity.Property(e => e.StrTipoProyecto)
                .HasMaxLength(10)
                .HasColumnName("str_tipo_proyecto");

            entity.HasOne(d => d.SkElementoNavigation).WithMany(p => p.RelacionUbicacionElementos)
                .HasForeignKey(d => d.SkElemento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_RELAC__sk_el__2E11BAA1");

            entity.HasOne(d => d.SkUbicacionesUnidadNavigation).WithMany(p => p.RelacionUbicacionElementos)
                .HasForeignKey(d => d.SkUbicacionesUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_RELAC__sk_ub__2F05DEDA");
        });

        modelBuilder.Entity<UbicacionesUnidad>(entity =>
        {
            entity.HasKey(e => e.SkUbicacion).HasName("PK__TBL_UBIC__816053E0A8BBD949");

            entity.ToTable("TBL_UBICACIONES_UNIDAD", "form");

            entity.Property(e => e.SkUbicacion).HasColumnName("sk_ubicacion");
            entity.Property(e => e.StrNombreUbicacion).HasColumnName("str_nombre_ubicacion");
            entity.Property(e => e.StrTipoProyecto)
                .HasMaxLength(10)
                .HasDefaultValue("AMBOS")
                .HasColumnName("str_tipo_proyecto");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.SkCategoria).HasName("PK__TBL_CATE__7D0F8C2B11894896");

            entity.ToTable("TBL_CATEGORIA", "form");

            entity.Property(e => e.SkCategoria).HasColumnName("sk_categoria");
            entity.Property(e => e.StrNombreCategoria).HasColumnName("str_nombre_categoria");
        });

        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => e.SkDetalle);

            entity.ToTable("TBL_DETALLE", "form");

            entity.Property(e => e.SkDetalle).HasColumnName("sk_detalle");
            entity.Property(e => e.SkCategoria).HasColumnName("sk_categoria");
            entity.Property(e => e.StrNombreDetalle).HasColumnName("str_nombre_detalle");

            entity.HasOne(d => d.SkCategoriaNavigation).WithMany(p => p.TblDetalles)
                .HasForeignKey(d => d.SkCategoria)
                .HasConstraintName("FK_TBL_DETALLE_TBL_CATEGORIA");
        });
        modelBuilder.Entity<RequestInfoCategoria>(entity =>
        {
            entity.HasKey(e => e.SkInfoCategoriaDetalle);

            entity.ToTable("TBL_RESPONSE_INFO_CATEGORIA_DETALLE", "form");

            entity.Property(e => e.SkInfoCategoriaDetalle).HasColumnName("sk_info_categoria_detalle");
            entity.Property(e => e.SkInfoElemento).HasColumnName("sk_info_elemento");
            entity.Property(e => e.StrNombreCategoria).HasColumnName("str_nombre_categoria");
            entity.Property(e => e.StrNombreDetalle).HasColumnName("str_nombre_detalle");
            entity.Property(e => e.StrObservacion).HasColumnName("str_observacion");
            entity.Property(e => e.StrUriImagen).HasColumnName("str_uri_imagen");

            entity.HasOne(d => d.SkInfoElementoNavigation).WithMany(p => p.ResponseInfoCategoriaDetalles)
                .HasForeignKey(d => d.SkInfoElemento)
                .HasConstraintName("FK_TBL_RESPONSE_INFO_CATEGORIA_DETALLE_TBL_RESPONSE_INFO_ELEMENTO");
        });

        modelBuilder.Entity<RequestInfoElemento>(entity =>
        {
            entity.HasKey(e => e.SkInfoElemento);

            entity.ToTable("TBL_RESPONSE_INFO_ELEMENTO", "form");

            entity.Property(e => e.SkInfoElemento).HasColumnName("sk_info_elemento");
            entity.Property(e => e.SkInfoUbicacion).HasColumnName("sk_info_ubicacion");
            entity.Property(e => e.StrNombreElemento).HasColumnName("str_nombre_elemento");

            entity.HasOne(d => d.SkInfoUbicacionNavigation).WithMany(p => p.ResponseInfoElementos)
                .HasForeignKey(d => d.SkInfoUbicacion)
                .HasConstraintName("FK_TBL_RESPONSE_INFO_ELEMENTO_TBL_RESPONSE_INFO_UBICACION");
        });

        modelBuilder.Entity<RequestInfoUbicacion>(entity =>
        {
            entity.HasKey(e => e.SkInfoUbicacion);

            entity.ToTable("TBL_RESPONSE_INFO_UBICACION", "form");

            entity.Property(e => e.SkInfoUbicacion).HasColumnName("sk_info_ubicacion");
            entity.Property(e => e.SkInfoUnidad).HasColumnName("sk_info_unidad");
            entity.Property(e => e.StrNombreUbicacion).HasColumnName("str_nombre_ubicacion");

            entity.HasOne(d => d.SkInfoUnidadNavigation).WithMany(p => p.ResponseInfoUbicaciones)
                .HasForeignKey(d => d.SkInfoUnidad)
                .HasConstraintName("FK_TBL_RESPONSE_INFO_UBICACION_TBL_RESPONSE_INFO_UNIDAD");
        });

        modelBuilder.Entity<RequestInfoUnidad>(entity =>
        {
            entity.HasKey(e => e.SkInfoUnidad).HasName("PK__TBL_RESP__D4A5A3EFD2C2BAEF");

            entity.ToTable("TBL_RESPONSE_INFO_UNIDAD", "form");

            entity.Property(e => e.SkInfoUnidad).HasColumnName("sk_info_unidad");
            entity.Property(e => e.DateFechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("date_fecha_entrega");
            entity.Property(e => e.DateFechaProgramada).HasColumnName("date_fecha_programada");
            entity.Property(e => e.StrEmail).HasColumnName("str_email");
            entity.Property(e => e.StrNombreUser).HasColumnName("str_nombre_user");
            entity.Property(e => e.StrProyecto).HasColumnName("str_proyecto");
            entity.Property(e => e.StrTipoEntrega).HasColumnName("str_tipo_entrega");
            entity.Property(e => e.StrTipoProyecto).HasColumnName("str_tipo_proyecto");
            entity.Property(e => e.StrUnidad).HasColumnName("str_unidad");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}