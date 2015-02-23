#include<stdio.h>
#include<windows.h>
#include<conio.h>

using namespace std;

int main()
{
    char remove_attribute[100];
    printf("\n\n\n\tENTER THE INFECTED DRIVE : ");
    strcpy(remove_attribute,"attrib -h -s -r -a  :\\*.* /s /d");
    remove_attribute[19]=getch();
    printf("%c\n\n",remove_attribute[19]);
    system(remove_attribute);
    printf("\n\n");
    char delete_shortcuts1[]="DEL  :\\*.ini";
    delete_shortcuts1[4]=remove_attribute[19];
    system(delete_shortcuts1);
    printf("\n\n");
    char delete_shortcuts2[]="DEL  :\\*.lnk";
    delete_shortcuts2[4]=remove_attribute[19];
    system(delete_shortcuts2);
    printf("\n\n");
    char delete_shortcuts3[]="DEL  :\\*.inf";
    delete_shortcuts3[4]=remove_attribute[19];
    system(delete_shortcuts3);
    printf("\n\n");
    char delete_shortcuts4[]="DEL  :\\*.db";
    delete_shortcuts4[4]=remove_attribute[19];
    system(delete_shortcuts4);
    printf("\n\n");
    char delete_shortcuts5[]="DEL  :\\*.vbs";
    delete_shortcuts5[4]=remove_attribute[19];
    system(delete_shortcuts5);
    printf("\n\n");
    printf("\n\n\tYOUR DRIVE IS SUCCESSFULLY RECOVERED\n");
    /*printf("\n\n\tDEEP DISINFECTION WILL TAKE A LONG TIME........\n\n\tDO YOU WANT A DEEP DISINFECTION.....");
    char ch = getch();
    '\n'
    if(ch == 'y' || ch == 'Y')
    {
        system("MD C:\\TEMP");
        printf("\n\n");
        char move_files[]="MOVE I:\\*.* C:\\TEMP";
        move_files[5]=remove_attribute[19];
        system(move_files);
        printf("\n\n");
        char format[100]="FORMAT  : /Q";
        format[7]=remove_attribute[19];
        system(format);
        printf("\n\n");
        char return_files[]="MOVE C:\\TEMP\\*.* I:";
        return_files[17]=remove_attribute[19];
        system(return_files);
        printf("\n\n");
        system("RMDIR C:\\TEMP\\");
    }*/
    printf("\n\n\tHOPE IT WAS USEFUL \n\n\t\tby KARTHIK M A M\n\n\n");
    system("PAUSE");
}
