﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asimov
{
    public partial class Index : Form
    {
        // importation des methods
        Methods methods = new Methods();

        // initialisation
        public Index()
        {
            InitializeComponent();

            // recuperer data user
            var data = methods.getRequest("/");
            label_nomUser.Text = data["user_info"]["user_prenom"].ToString() + " " + data["user_info"]["user_nom"].ToString();
            // if user info is responsable
            if (data["user_info"]["user_isProviseur"].ToString() == "1")
            {
                label_statutUser.Text = "Proviseur";
            }
            else if (data["user_info"]["user_isAdministration"].ToString() == "1")
            {
                label_statutUser.Text = "Administration";
                btn_notes.Visible = false;
                btn_evaluations.Visible = false;
                
            } else if (data["user_info"]["user_isProf"].ToString() == "1")
            {
                label_statutUser.Text = "Professeur";
                btn_notes.Visible = false;
                btn_profs.Visible = false;
                btn_eleves.Visible = false;
                btn_matieres.Visible = false;
            } else
            {
                label_statutUser.Text = "Élève";
                btn_evaluations.Visible = false;
                btn_classes.Visible = false;
                btn_profs.Visible = false;
                btn_eleves.Visible = false;
                btn_matieres.Visible = false;
            }
        }

        // les notes
        private void btn_notes_Click(object sender, EventArgs e)
        {
            // ouvrir notes
            Notes form = new Notes();
            methods.openForm("/notes/fiche_eleve/0/0", this, form);
        }

        // les évaluations
        private void btn_evaluations_Click(object sender, EventArgs e)
        {
            // ouvrir évaluations
            Liste form = new Liste("Les évaluations", "/evaluations", "EVA");
            methods.openForm("/evaluations/liste", this, form);
        }

        // les classes
        private void btn_classes_Click(object sender, EventArgs e)
        {
            // ouvrir classes
            Liste form = new Liste("Les classes", "/classes", "CLA");
            methods.openForm("/classes/liste", this, form);
        }

        // les profs
        private void btn_profs_Click(object sender, EventArgs e)
        {
            // ouvrir profs
            Liste form = new Liste("Les professeurs", "/profs", "PRO");
            methods.openForm("/profs/liste", this, form);
        }

        // les eleves
        private void btn_eleves_Click(object sender, EventArgs e)
        {
            // ouvrir eleves
            Liste form = new Liste("Les elèves", "/eleves", "ELE");
            methods.openForm("/eleves/liste", this, form);
        }

        // les matieres
        private void btn_matieres_Click(object sender, EventArgs e)
        {
            // ouvrir matieres
            Liste form = new Liste("Les matières", "/matieres", "MAT");
            methods.openForm("/matieres/liste", this, form);
        }
        
        // les responsables
        private void btn_responsables_Click(object sender, EventArgs e)
        {
            // ouvrir eleves
            Liste form = new Liste("Les responsables", "/responsables", "RES");
            methods.openForm("/responsables/liste", this, form);
        }

        // deconnexion
        private void btn_deconnexion_Click(object sender, EventArgs e)
        {
            // requete deconnexion
            string url = "/deconnexion";
            var data = methods.getRequest(url);

            // si valid
            if (data["valid"] != null)
            {
                MessageBox.Show(data["valid"][0].ToString(), "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(data["erreur"][0].ToString(), "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // revenir page de connexion
            this.Hide();
            Connexion connexion = new Connexion();
            connexion.Show();
        }

    }
}
