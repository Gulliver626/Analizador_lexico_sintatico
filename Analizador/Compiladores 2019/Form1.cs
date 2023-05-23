using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Compiladores_2019
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                        
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {

            
           dataGridView1.Rows.Clear();
                  
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                        this.Analizar();

                      
        }



        string unir_cad = "";
        string espacio = "[' ']";
        string salto = "['\n']";
     
        string unir_string = "";
        string unir_com = "";

        int contar_columas = 1;
        int contar_lineas = 1;
        
        public void insertartexto()
        {
            openFileDialog1.Filter = "TextFile | *.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string spath = openFileDialog1.FileName;
                StreamReader SR = new StreamReader(spath);
                string data = SR.ReadToEnd();
                textBox1.Text = data;

            }
        }

        public void Analizar()
        {
            dataGridView1.Rows.Clear();

            contar_columas = 1;
            contar_lineas = 1;


            char validar_com = '0';
            char validar_cad_string = '0';

            string comentario = "[//]";
            string cad_string = "[”|”]";
            string texto = textBox1.Text;



            foreach (char letra in texto)
            {
                string letra2 = letra.ToString();           


                if (Regex.IsMatch(letra2, cad_string))
                {
                    if (validar_cad_string.Equals('0'))
                    {
                        validar_cad_string = '1';
                    }
                    else
                    {
                        
                        dataGridView1.Rows.Add(unir_string + "”", "mensaje", contar_lineas, contar_columas);
                        //contar_lineas += 1;
                        validar_cad_string = '0';
                        unir_string = "";


                    }
                }

                if (validar_cad_string.Equals('1'))
                {
                    unir_string = unir_string + letra2;
                }


                if (Regex.IsMatch(letra2, comentario))
                {
                    validar_com = '1';
                }

                if (validar_com.Equals('1'))
                {
                    unir_com = unir_com + letra2;

                    if (letra.Equals('\n'))
                    {

                        dataGridView1.Rows.Add(unir_com + "", "comentario", contar_lineas, contar_columas);
                        contar_lineas += 1;
                        contar_columas = 1;
                        validar_com = '0';
                        unir_com = "";


                    }

                }

                else if (validar_com.Equals('0') & validar_cad_string.Equals('0') & letra2 != "\"" & letra2 != "\r")
                {
                    if (letra2 == " " || letra2=="\n")
                    {
                        this.AnalizarPalabras();
                       
                        if (Regex.IsMatch(letra2, espacio))
                        {
                            contar_columas += 1;
                        }
                        if (Regex.IsMatch(letra2, salto))
                        {
                            contar_lineas += 1;
                            contar_columas = 1;
                        }
                    }
                    else
                    {
                        unir_cad = unir_cad + letra2;
                    }
                    
                }


            } //fin foreach

        }


        public void AnalizarPalabras()
        {
            this.VerificarLexema();          
        }


        char validar_uno_mas = '0';
        public void VerificarLexema()
        {


            string[] reservado = {  "main", "include", "std", "cout", "cin", "endl", "return", "iostream.h", "conio.h", 
                    "assert.h", "stdio.h", "if","else","while","for","namespace" };
            string[] identificador = { "suma","resta","multiplicacion","division","resultado"};
            string[] operador = { ">>", "<<", "::", "=", "-"};
            string[] tipodato = { "int", "float", "char", "short", "long", "double", "bool","void","string" };
            string exp_numeros = "^[0-9]+$[0-9]?";
            string exp_delimitador = "^[;|(|)|{|}|”]$";
            string exp_operadores = "^[+|/|*]$";
            string asignacion = "^#$";
            string exp_comparador = "^[<|>]$|^==$";
            string variable = "^numero[(A-Z)?]$";
            string ident = "^[a-z]$";                   
           

            char validar_reservado = '0';



            for (int i = 0; i < 16; i++)
            {
                if (unir_cad.Equals(reservado[i]))
                {
                    dataGridView1.Rows.Add(unir_cad + "", "reservada", contar_lineas, contar_columas);
                    validar_reservado = '1';
                    if (Regex.IsMatch(unir_cad, "si"))
                    {
                        validar_uno_mas = '1';
                    }
                }

            }
            for (int i = 0; i < 5; i++)
            {
                if (unir_cad.Equals(identificador[i]))
                {
                    dataGridView1.Rows.Add(unir_cad + "", "variable", contar_lineas, contar_columas);
                    validar_reservado = '1';
                    if (Regex.IsMatch(unir_cad, "si"))
                    {
                        validar_uno_mas = '1';
                    }
                }

            }

            for (int i = 0; i < 5; i++)
            {
                if (unir_cad.Equals(operador[i]))
                {
                    dataGridView1.Rows.Add(unir_cad + "", "operador", contar_lineas, contar_columas);
                    validar_reservado = '1';
                    if (Regex.IsMatch(unir_cad, "si"))
                    {
                        validar_uno_mas = '1';
                    }
                }

            }
            for (int i = 0; i < 9; i++)
            {
                if (unir_cad.Equals(tipodato[i]))
                {
                    dataGridView1.Rows.Add(unir_cad + "", "tipo de dato", contar_lineas, contar_columas);
                    validar_reservado = '1';
                    if (Regex.IsMatch(unir_cad, "si"))
                    {
                        validar_uno_mas = '1';
                    }
                }

            }

            if (Regex.IsMatch(unir_cad, exp_numeros))
            {
                dataGridView1.Rows.Add(unir_cad + "", "numero", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, exp_delimitador))
            {
                dataGridView1.Rows.Add(unir_cad + "", "delimitador", contar_lineas, contar_columas);

            }
            else if (Regex.IsMatch(unir_cad, exp_operadores))
            {
                dataGridView1.Rows.Add(unir_cad + "", "operador", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, asignacion))
            {
                dataGridView1.Rows.Add(unir_cad + "", "asignacion", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, exp_comparador))
            {
                dataGridView1.Rows.Add(unir_cad + "", "comparador", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, variable))
            {
                dataGridView1.Rows.Add(unir_cad + "", "variable", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, ident))
            {
                dataGridView1.Rows.Add(unir_cad + "", "variable", contar_lineas, contar_columas);
            }

            else if (validar_reservado.Equals('0') & unir_cad != "" & unir_cad != "\"")
            {

                dataGridView1.Rows.Add(unir_cad + "", "error de lexema", contar_lineas, contar_columas);

            }
            unir_cad = "";
        }





       private void Form1_Load(object sender, EventArgs e)
       {

       }
      
       private void richTextBox1_TextChanged(object sender, EventArgs e)
       {

          
       }



        private void button2_Click(object sender, EventArgs e)
        {
            this.insertartexto();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
