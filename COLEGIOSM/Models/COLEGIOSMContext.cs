using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace COLEGIOSM.Models
{
    public partial class COLEGIOSMContext : DbContext
    {
        public COLEGIOSMContext()
        {
        }

        public COLEGIOSMContext(DbContextOptions<COLEGIOSMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Apoderado> Apoderado { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Matricula> Matricula { get; set; }
  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-U2BB04Q;Initial Catalog=COLEGIOSM;User Id=Arturo;Password=andre");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("ADMIN");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.Idalumno)
                    .HasName("PK__ALUMNO__2A8067BFE4E73569");

                entity.ToTable("ALUMNO");

                entity.Property(e => e.Idalumno).HasColumnName("IDAlumno");

                entity.Property(e => e.Apellidos)
                    .HasColumnName("apellidos")
                    .HasMaxLength(100);

                entity.Property(e => e.Ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(100);

                entity.Property(e => e.Codigo)
                    .HasColumnName("codigo")
                    .HasMaxLength(10);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(100);

                entity.Property(e => e.Dni)
                    .HasColumnName("dni")
                    .HasMaxLength(8);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("date");

                entity.Property(e => e.Idapoderado).HasColumnName("IDApoderado");

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.Nombres)
                    .HasColumnName("nombres")
                    .HasMaxLength(100);

                entity.Property(e => e.Sexo)
                    .HasColumnName("sexo")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdapoderadoNavigation)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.Idapoderado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ALUMNO__IDApoder__2C3393D0");
            });

            modelBuilder.Entity<Apoderado>(entity =>
            {
                entity.HasKey(e => e.Idapoderado)
                    .HasName("PK__APODERAD__6AA7CEC67DC2D435");

                entity.ToTable("APODERADO");

                entity.Property(e => e.Idapoderado).HasColumnName("IDApoderado");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(100);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(100);

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasColumnName("dni")
                    .HasMaxLength(8);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnName("nombres")
                    .HasMaxLength(100);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasColumnName("sexo")
                    .HasMaxLength(100);

                entity.Property(e => e.TipoRelacion)
                    .IsRequired()
                    .HasColumnName("tipoRelacion")
                    .HasMaxLength(100);
            });

            //modelBuilder.Entity<Asistencia>(entity =>
            //{
            //    entity.HasKey(e => e.Idasistencia)
            //        .HasName("PK__ASISTENC__89B14B23798462A3");

            //    entity.ToTable("ASISTENCIA");

            //    entity.Property(e => e.Idasistencia).HasColumnName("IDAsistencia");

            //    entity.Property(e => e.Estado).HasColumnName("estado");

            //    entity.Property(e => e.FechaRegistro)
            //        .HasColumnName("fechaRegistro")
            //        .HasColumnType("datetime");

            //    entity.Property(e => e.Idalumno).HasColumnName("IDAlumno");

            //    entity.Property(e => e.Idcurso).HasColumnName("IDCurso");

            //    entity.Property(e => e.Idgrado).HasColumnName("IDGrado");

            //    entity.HasOne(d => d.IdalumnoNavigation)
            //        .WithMany(p => p.Asistencia)
            //        .HasForeignKey(d => d.Idalumno)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__ASISTENCI__IDAlu__31EC6D26");

            //    entity.HasOne(d => d.IdcursoNavigation)
            //        .WithMany(p => p.Asistencia)
            //        .HasForeignKey(d => d.Idcurso)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__ASISTENCI__IDCur__32E0915F");

            //    entity.HasOne(d => d.IdgradoNavigation)
            //        .WithMany(p => p.Asistencia)
            //        .HasForeignKey(d => d.Idgrado)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__ASISTENCI__IDGra__33D4B598");
            //});

            //modelBuilder.Entity<Curso>(entity =>
            //{
            //    entity.HasKey(e => e.Idcurso)
            //        .HasName("PK__CURSO__9437DBA53EAB88A1");

            //    entity.ToTable("CURSO");

            //    entity.Property(e => e.Idcurso).HasColumnName("IDCurso");

            //    entity.Property(e => e.Descripcion)
            //        .IsRequired()
            //        .HasColumnName("descripcion")
            //        .HasMaxLength(100);

            //    entity.Property(e => e.Estado).HasColumnName("estado");

            //    entity.Property(e => e.FechaRegistro)
            //        .HasColumnName("fechaRegistro")
            //        .HasColumnType("datetime");

            //    entity.Property(e => e.Nombre)
            //        .IsRequired()
            //        .HasColumnName("nombre")
            //        .HasMaxLength(100);
            //});

            //modelBuilder.Entity<Grado>(entity =>
            //{
            //    entity.HasKey(e => e.Idgrado)
            //        .HasName("PK__GRADO__CEDFC9F76B9EC52B");

            //    entity.ToTable("GRADO");

            //    entity.Property(e => e.Idgrado).HasColumnName("IDGrado");

            //    entity.Property(e => e.Descripcion)
            //        .IsRequired()
            //        .HasColumnName("descripcion")
            //        .HasMaxLength(100);

            //    entity.Property(e => e.Estado).HasColumnName("estado");

            //    entity.Property(e => e.FechaRegistro)
            //        .HasColumnName("fechaRegistro")
            //        .HasColumnType("date");

            //    entity.Property(e => e.Idnivel).HasColumnName("IDNivel");

            //    entity.Property(e => e.Nombre)
            //        .IsRequired()
            //        .HasColumnName("nombre")
            //        .HasMaxLength(100);

            //    entity.HasOne(d => d.IdnivelNavigation)
            //        .WithMany(p => p.Grado)
            //        .HasForeignKey(d => d.Idnivel)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__GRADO__IDNivel__2F10007B");
            //});

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__LOGS__5E54864820CEB161");

                entity.ToTable("LOGS");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Method)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.HasKey(e => e.Idmatricula)
                    .HasName("PK__MATRICUL__3604C97DD7C4D861");

                entity.ToTable("MATRICULA");

                entity.Property(e => e.Idmatricula).HasColumnName("IDMatricula");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("codigo")
                    .HasMaxLength(10);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idalumno).HasColumnName("IDAlumno");

                entity.Property(e => e.Idapoderado).HasColumnName("IDApoderado");

                entity.Property(e => e.Ieprocedencia)
                    .IsRequired()
                    .HasColumnName("IEProcedencia")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdapoderadoNavigation)
                    .WithMany(p => p.Matricula)
                    .HasForeignKey(d => d.Idapoderado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MATRICULA__IDApo__36B12243");
            });

            //modelBuilder.Entity<Nivel>(entity =>
            //{
            //    entity.HasKey(e => e.Idnivel)
            //        .HasName("PK__NIVEL__750FE9B30A53488F");

            //    entity.ToTable("NIVEL");

            //    entity.Property(e => e.Idnivel).HasColumnName("IDNivel");

            //    entity.Property(e => e.DescripcionNivel)
            //        .IsRequired()
            //        .HasColumnName("descripcionNivel")
            //        .HasMaxLength(100);

            //    entity.Property(e => e.DescripcionTurno)
            //        .IsRequired()
            //        .HasColumnName("descripcionTurno")
            //        .HasMaxLength(100);

            //    entity.Property(e => e.Estado).HasColumnName("estado");

            //    entity.Property(e => e.FechaRegistro)
            //        .HasColumnName("fechaRegistro")
            //        .HasColumnType("date");

            //    entity.Property(e => e.HorarioFin)
            //        .IsRequired()
            //        .HasColumnName("horarioFin")
            //        .HasMaxLength(100);

            //    entity.Property(e => e.HorarioInicio)
            //        .IsRequired()
            //        .HasColumnName("horarioInicio")
            //        .HasMaxLength(100);
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
