﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Banking_Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            Data_Access_Layer dal = Data_Access_Layer.getInstance();
            dal.loadBankAccounts();
            bool running = true;

            do
            {

                Console.WriteLine("");
                Console.WriteLine("***Banking Application Menu***");
                Console.WriteLine("1. Add Bank Account");
                Console.WriteLine("2. Close Bank Account");
                Console.WriteLine("3. View Account Information");
                Console.WriteLine("4. Make Lodgement");
                Console.WriteLine("5. Make Withdrawal");
                Console.WriteLine("6. Exit");
                Console.WriteLine("CHOOSE OPTION:");
                String option = Console.ReadLine();
                StringBuilder accNo = new StringBuilder();
                switch(option)
                {
                    case "1":
                        String accountType = "";
                        int loopCount = 0;
                        
                        do
                        {

                           if(loopCount > 0)
                                Console.WriteLine("INVALID OPTION CHOSEN - PLEASE TRY AGAIN");

                            Console.WriteLine("");
                            Console.WriteLine("***Account Types***:");
                            Console.WriteLine("1. Current Account.");
                            Console.WriteLine("2. Savings Account.");
                            Console.WriteLine("CHOOSE OPTION:");
                            accountType = Console.ReadLine();

                            loopCount++;

                        } while (!(accountType.Equals("1") || accountType.Equals("2")));

                        StringBuilder name = new StringBuilder();
                        loopCount = 0;

                        do
                        {
                            name.Clear();
                            if (loopCount > 0)
                                Console.WriteLine("INVALID NAME ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Name: ");
                            name.Append(Console.ReadLine());

                            loopCount++;

                        } while (name.Equals(""));

                        StringBuilder addressLine1 = new StringBuilder();
                        loopCount = 0;

                        do
                        {
                            addressLine1.Clear();
                            if (loopCount > 0)
                                Console.WriteLine("INVALID ÀDDRESS LINE 1 ENTERED - PLEASE TRY AGAIN");

                             Console.WriteLine("Enter Address Line 1: ");
                            addressLine1.Append(Console.ReadLine());

                            loopCount++;

                        } while (addressLine1.Equals(""));

                        Console.WriteLine("Enter Address Line 2: ");
                        String addressLine2 = Console.ReadLine();
                        
                        Console.WriteLine("Enter Address Line 3: ");
                        String addressLine3 = Console.ReadLine();

                        StringBuilder town = new StringBuilder();
                        loopCount = 0;

                        do
                        {
                            town.Clear();
                            if (loopCount > 0)
                                Console.WriteLine("INVALID TOWN ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Town: ");
                            town.Append(Console.ReadLine());

                            loopCount++;

                        } while (town.Equals(""));

                        double balance = -1;
                        loopCount = 0;

                        do
                        {

                            if (loopCount > 0)
                                Console.WriteLine("INVALID OPENING BALANCE ENTERED - PLEASE TRY AGAIN");

                            Console.WriteLine("Enter Opening Balance: ");
                            String balanceString = Console.ReadLine();

                            try
                            {
                                balance = Convert.ToDouble(balanceString);
                            }

                            catch 
                            {
                                loopCount++;
                            }

                        } while (balance < 0);

                        Bank_Account ba;

                        if (Convert.ToInt32(accountType) == Account_Type.Current_Account)
                        {
                            double overdraftAmount = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID OVERDRAFT AMOUNT ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Overdraft Amount: ");
                                String overdraftAmountString = Console.ReadLine();

                                try
                                {
                                    overdraftAmount = Convert.ToDouble(overdraftAmountString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (overdraftAmount < 0);

                            ba = new Current_Account(name.ToString(), addressLine1.ToString(), addressLine2, addressLine3, town.ToString(), balance, overdraftAmount);
                            name.Clear();
                            addressLine1.Clear();
                            town.Clear();
                        }

                        else
                        {

                            double interestRate = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID INTEREST RATE ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Interest Rate: ");
                                String interestRateString = Console.ReadLine();

                                try
                                {
                                    interestRate = Convert.ToDouble(interestRateString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (interestRate < 0);
                            
                            ba = new Savings_Account(name.ToString(), addressLine1.ToString(), addressLine2, addressLine3, town.ToString(), balance, interestRate);
                            name.Clear();
                            addressLine1.Clear();
                            town.Clear();
                        }

                        accNo.Append(dal.addBankAccount(ba));
                       
                        Console.WriteLine("New Account Number Is: " + accNo.ToString());
                        accNo.Clear();
                        break;
                    case "2":

                        accNo.Clear();

                        Console.WriteLine("Enter Account Number: ");
                        accNo.Append(Console.ReadLine());
                    
                        ba = dal.findBankAccountByAccNo(accNo.ToString());

                        if (ba is null)
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            Console.WriteLine(ba.ToString());

                            String ans = "";

                            do
                            {

                                Console.WriteLine("Proceed With Delection (Y/N)?"); 
                                ans = Console.ReadLine();

                                switch (ans)
                                {
                                    case "Y":
                                    case "y": dal.closeBankAccount(accNo.ToString());
                                        accNo.Clear();
                                        break;
                                    case "N":
                                    case "n":
                                        break;
                                    default:
                                        Console.WriteLine("INVALID OPTION CHOSEN - PLEASE TRY AGAIN");
                                        break;
                                }
                            } while (!(ans.Equals("Y") || ans.Equals("y") || ans.Equals("N") || ans.Equals("n")));
                        }
                       
                        break;
                    case "3":
                        accNo.Clear();
                        Console.WriteLine("Enter Account Number: ");
                        accNo.Append(Console.ReadLine());

                        ba = dal.findBankAccountByAccNo(accNo.ToString());
                        accNo.Clear();
                        if(ba is null) 
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            Console.WriteLine(ba.ToString());
                        }
                       
                        break;
                    case "4": //Lodge
                        accNo.Clear();
                        Console.WriteLine("Enter Account Number: ");
                        accNo.Append(Console.ReadLine());

                        ba = dal.findBankAccountByAccNo(accNo.ToString());

                        if (dal.GetType() != typeof (Data_Access_Layer)) //change made(add for bank account in program)
                        {
                            Console.WriteLine("ERROR IMPROPER ACCESS!");
                        }
                        else if (ba is null)
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            double amountToLodge = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID AMOUNT ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Amount To Lodge: ");
                                String amountToLodgeString = Console.ReadLine();

                                try
                                {
                                    amountToLodge = Convert.ToDouble(amountToLodgeString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (amountToLodge < 0);

                            dal.lodge(accNo.ToString(), amountToLodge);
                            accNo.Clear();
                        }
                        
                        break;
                    case "5": //Withdraw
                        accNo.Clear();
                        Console.WriteLine("Enter Account Number: ");
                        accNo.Append(Console.ReadLine());

                        ba = dal.findBankAccountByAccNo(accNo.ToString());

                        if (ba is null)
                        {
                            Console.WriteLine("Account Does Not Exist");
                        }
                        else
                        {
                            double amountToWithdraw = -1;
                            loopCount = 0;

                            do
                            {

                                if (loopCount > 0)
                                    Console.WriteLine("INVALID AMOUNT ENTERED - PLEASE TRY AGAIN");

                                Console.WriteLine("Enter Amount To Withdraw (€" + ba.getAvailableFunds() + " Available): ");
                                String amountToWithdrawString = Console.ReadLine();

                                try
                                {
                                    amountToWithdraw = Convert.ToDouble(amountToWithdrawString);
                                }

                                catch
                                {
                                    loopCount++;
                                }

                            } while (amountToWithdraw < 0);

                            bool withdrawalOK = dal.withdraw(accNo.ToString(), amountToWithdraw);
                            accNo.Clear();
                            if(withdrawalOK == false)
                            {

                                Console.WriteLine("Insufficient Funds Available.");
                            }
                            
                        }
                        accNo.Clear();
                        break;
                    case "6":
                        accNo.Clear();
                        running = false;
                        break;
                    default:    
                        Console.WriteLine("INVALID OPTION CHOSEN - PLEASE TRY AGAIN");
                        break;
                }

                accNo.Clear();
            } while (running != false);

        }

    }
}