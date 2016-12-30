namespace Cait.Bitcoin.Net.Constants
{
    /*

        Known magic values:

        Network	    Magic value     Sent over wire as
        main        0xD9B4BEF9      F9 BE B4 D9
        testnet     0xDAB5BFFA      FA BF B5 DA
        testnet3	0x0709110B      0B 11 09 07
        namecoin	0xFEB4BEF9      F9 BE B4 FE

    */

    public enum Magic : uint
    {
        Main = 0xD9B4BEF9,
        TestNet = 0xDAB5BFFA,
        TestNet3 = 0x0709110B,
        Namecoin = 0xFEB4BEF9
    }
}