using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.OpFlix.WebApi.Domains
{
    public partial class OpFlixContext : DbContext
    {
        public OpFlixContext()
        {
        }

        public OpFlixContext(DbContextOptions<OpFlixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<ClassificacoesIndicativas> ClassificacoesIndicativas { get; set; }
        public virtual DbSet<Lancamentos> Lancamentos { get; set; }
        public virtual DbSet<Plataformas> Plataformas { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Favoritos> Favoritos { get; set; }

        // Unable to generate entity type for table 'dbo.Favoritos'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress; Initial Catalog=M_OpFlix;User Id=sa;Pwd=132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favoritos>()
                .HasKey(p => new { p.IdUsuario, p.IdLancamento });
            modelBuilder.Entity<Favoritos>()
                .HasOne<Usuarios>(sc => sc.Usuario)
                .WithMany(s => s.Favoritos)
                .HasForeignKey(sc => sc.IdUsuario);

            modelBuilder.Entity<Favoritos>()
                .HasOne<Lancamentos>(sc => sc.Lancamento)
                .WithMany(s => s.Favoritos)
                .HasForeignKey(sc => sc.IdLancamento);

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.HasIndex(e => e.Categoria)
                    .HasName("UQ__Categori__08015F8B9A5FACE0")
                    .IsUnique();

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClassificacoesIndicativas>(entity =>
            {
                entity.HasKey(e => e.IdClassificacaoIndicativa);

                entity.Property(e => e.Ci)
                    .HasColumnName("CI")
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lancamentos>(entity =>
            {
                entity.HasKey(e => e.IdLancamento);

                entity.Property(e => e.DataDeLancamento)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Episodios).HasDefaultValueSql("((1))");

                entity.Property(e => e.Poster)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Sinopse)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.TempoDeDuracao).HasColumnType("time(0)");

                entity.Property(e => e.TipoDeMidia)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Lancament__IdCat__5535A963");

                entity.HasOne(d => d.IdClassificacaoIndicativaNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.IdClassificacaoIndicativa)
                    .HasConstraintName("FK__Lancament__IdCla__571DF1D5");

                entity.HasOne(d => d.IdPlataformaNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.IdPlataforma)
                    .HasConstraintName("FK__Lancament__IdPla__5629CD9C");
            });

            modelBuilder.Entity<Plataformas>(entity =>
            {
                entity.HasKey(e => e.IdPlataforma);

                entity.HasIndex(e => e.Plataforma)
                    .HasName("UQ__Platafor__3FCED092C48E06C0")
                    .IsUnique();

                entity.Property(e => e.Plataforma)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.HasKey(e => e.IdReview);

                entity.Property(e => e.Review).HasColumnType("text");

                entity.HasOne(d => d.IdLancamentoNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdLancamento)
                    .HasConstraintName("FK__Reviews__IdLanca__4F47C5E3");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Reviews__IdUsuar__4E53A1AA");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.HasIndex(e => e.Email)
                    .HasName("Email")
                    .IsUnique();

                entity.HasIndex(e => e.NomeDeUsuario)
                    .HasName("UQ__Usuarios__34DF13F6E542E392")
                    .IsUnique();

                entity.Property(e => e.DataDeNascimento).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagemUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('https://image.flaticon.com/icons/svg/17/17004.svg')");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NomeDeUsuario)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('U')");
            });
        }
    }
}
