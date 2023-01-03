﻿using SSD_Assignment___Banking_Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Application
{
     internal abstract class Bank_Account
     {

        public String accountNo;
        public String name;
        public String address_line_1;
        public String address_line_2;
        public String address_line_3;
        public String town;
        public double balance;



        public Bank_Account()
        {

        }
        
        public Bank_Account(String name, String address_line_1, String address_line_2, String address_line_3, String town, double balance)
        {
            this.accountNo = System.Guid.NewGuid().ToString();
            this.name = name;
            this.address_line_1 = address_line_1;
            this.address_line_2 = address_line_2;
            this.address_line_3 = address_line_3;
            this.town = town;
            this.balance = balance;
        }

        public void lodge(double amountIn)
        {

            balance += amountIn;

        }

        public abstract bool withdraw(double amountToWithdraw);

        public abstract double getAvailableFunds();

        public override String ToString()
        {

            EncryptDecrypt En = new EncryptDecrypt();
            return "\nAccount No: " + accountNo + "\n" +
            "Name: " + En.Decrpyt(name.ToString()).ToString() + "\n" +
            "Address Line 1: " + En.Decrpyt(address_line_1.ToString()).ToString() + "\n" +
            "Address Line 2: " + address_line_2 + "\n" +
            "Address Line 3: " + address_line_3 + "\n" +
            "Town: " + En.Decrpyt(town.ToString()).ToString() + "\n" +
            "Balance: " + balance + "\n";

        }

    }
}
