# NET.S.2018.Kononenko.BankAccount
Project about bank account

Develop a type system for describing the work with a bank account. The status of the account is determined by its number, data on the account holder (name, surname, e-mail), the amount on the account and some bonus points that increase / decrease each time you replenish the account / write-offs to the amounts for replenishment and write-off and settlement in depending on certain values ​​of the "value" of the balance and "value" of the replenishment. Values ​​of the "value" of the balance and the "value" of the replenishment, consisting of the number of accounts that can be, for example, Base, Gold, Platinum. To work with the following related features:

perform replenishment to the account;
execute an account write-off;
create a new account;
close an account.
To store information about accounts, use fake-implementation of the repository (repository) as a wrapper class for the List collection.

The work of the classes is demonstrated on the example of the console application.

When designing types of exercises the following options for expanding / changing functionality

adding new types of accounts;
changing / adding sources for storing information about accounts;
change the logic for calculating bonus points;
change the logic for generating account numbers.

To resolve the dependencies, use the Ninject-framework.

Test the Bll layer (NUnit and Moq frameworks).
