using serviceReport.Models.Auditoria;
using serviceReport.Models.ISO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace serviceReport.Models.AccessBD
{
    public class AuditoriaContext : DbContext
    {
        public AuditoriaContext() : base("auditBd") { }

        #region ISO
        public DbSet<Anexo> Anexos { get; set; }

        public DbSet<Dominio> Dominios { get; set; }

        public DbSet<Grupo> Grupos { get; set; }

        public DbSet<NivelCalificacion> NivelesCalificacion { get; set; }

        public DbSet<Pregunta> Preguntas { get; set; }

        public DbSet<Roll> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        #endregion

        #region Auditoria

        public DbSet<Auditoria.Auditoria> Auditorias { get; set; }

        public DbSet<AuditoriaRespuestas> Respuestas { get; set; }

        public DbSet<DominiosAuditoria> DominiosAuditoria { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<EstadoAuditoria> Estados { get; set; }

        #endregion
    }
}