﻿using Microsoft.Data.SqlClient;
using NetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace NetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresSQLServer: IRepositoryDoctores
    {
        private DataTable tableDoctores;
        private SqlConnection cn;
        private SqlCommand com;
     
        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.tableDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter
                ("select * from DOCTOR", connectionString);
            ad.Fill(this.tableDoctores);
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }

        public void InsertDoctor
            (int idDoctor, string apellido, string especialidad
            , int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (@idhospital, @iddoctor "
                + ", @apellido, @especialidad, @salario)";
            this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void DeleteDoctor(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            this.com.Parameters.AddWithValue("@iddoctor", idDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Doctor FindDoctor(int idDoctor)
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == idDoctor
                           select datos;
            var row = consulta.First();

            Doctor doc = new Doctor();
            doc.IdDoctor = row.Field<int>("DOCTOR_NO");
            doc.Apellido = row.Field<string>("APELLIDO");
            doc.Especialidad = row.Field<string>("ESPECIALIDAD");
            doc.Salario = row.Field<int>("SALARIO");
            doc.IdHospital = row.Field<int>("HOSPITAL_COD");

            return doc;
        }

        public void UpdateDoctor(Doctor doctor)
        {
            string sql = "SP_UPDATE_DOCTOR";
            this.com.Parameters.AddWithValue("@iddoctor", doctor.IdDoctor);
            this.com.Parameters.AddWithValue("@apellido", doctor.Apellido);
            this.com.Parameters.AddWithValue("@especialidad", doctor.Especialidad);
            this.com.Parameters.AddWithValue("@salario", doctor.Salario);
            this.com.Parameters.AddWithValue("@idhopsital", doctor.IdHospital); 
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public List<Doctor> BuscarDoctores(string especialidad)
        {
            var consulta = from datos in this.tableDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD") == especialidad
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.IdDoctor = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doc.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctores.Add(doc);
            }
            return doctores;
        }
    }
}
