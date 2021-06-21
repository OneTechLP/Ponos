# Hiring Challenge
## Backend Code

The prices are wrong!

Oh no! A team of accountants at a huge company have discovered that customers are
occasionally being charged too much or too little for various products.  They've
written you an angry email about it, and now you're tasked with coming up with
a data report about the situation.

They've sent you a directory containing the receipts in which they suspect
prices are wrong and a CSV file containing the product codes and the correct
price each one should have.

A receipt is just a plaintext file with rows formatted like:
- A product name
- A product code
- A price
- A random flag character (this exists for no other reason than to confuse you)

There's also a store number listed up top.

Sometimes, items might be voided.  This means that the previous line is null and
void, and the customer didn't pay for it, so it won't reflect in the total.  Your
program should ignore these voided products.

So, about that report...

The angry accountants would like you to write a program that outputs a CSV file with all the products that have been mischarged along with a count of the mischarges and a total of the mischarges:

Product Code | Count | Total
--------|--------|--------
123 | 23 | -32.56 
454 | 1 | +100.34 

Any products that have not had any wrong charges should not be in this list.  Please sort the list from low to high so the largest losses are first in the file.  

Your program should take in a parameter for directory so that the angry accountants can run it over and over again on a different set of receipts if they want.

