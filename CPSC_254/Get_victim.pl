#!/usr/bin/perl
my $path_to_file = "numbersToBeCalled.txt";
open my $handle, '<', $path_to_file;
chomp(my @arrayOfNumbers = <$handle>);
close $handle;
open(my $fileToWrite, ">numbersToBeCalled.txt");
open(my $logFile, ">>victimLog.txt");
my $count = 0;
my $valueToUse = 32;
my $userInput = $ARGV[0];
if($userInput >= 1)
{
        $valueToUse = $userInput;
}
$count = @arrayOfNumbers;
my $innerCount = 0;
my $random_number = int(rand($valueToUse)) + 1;
do
{
        $innerCount = 0;
        if($count == 0)
        {
                push @arrayOfNumbers, $random_number;
        }
        elsif($count != $valueToUse)
        {
                foreach(@arrayOfNumbers)
                {
                        if($_ == $random_number)
                        {
                                $innerCount = 1;
                                $random_number = int(rand($valueToUse)) + 1;
                        }
                }
                if($innerCount == 0)
                {
                        push @arrayOfNumbers, $random_number;
                        print $logFile "$random_number Student $count " .(localtime);
                        print $logFile "\n";
                }
        }
} while($innerCount == 1);
print $fileToWrite join("\n", @arrayOfNumbers);
print "$random_number Student $count\n";
if($count == $valueToUse)
{
        print "All students have been called, resetting counter...\n";
        unlink("numbersToBeCalled.txt");
}
