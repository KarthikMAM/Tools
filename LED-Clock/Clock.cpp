#include<stdio.h>
#include<conio.h>
#include<windows.h>

using namespace std;

int binary_converter(int n[6], int a[6][4])
{
    int j,i,temp;
    for(j=0;j<6;j++)
    {
        temp = n[j];
        for(int i=0;i<4;i++)
        {
            a[j][i]=temp%2;
            temp=temp/2;
        }
    }
}

int display(int a[6][4])
{
    int j;
    for(j=0;j<6;j++)
    {
        if( (!a[j][3] && a[j][1]) || (!a[j][3] && a[j][2] && !a[j][1] && a[j][0]) || ( (!a[j][2] && !a[j][1]) && (a[j][3] || !a[j][0]) ) )
            printf("\t _");
        else
            printf("\t  ");
        if(j%2!=0 && j!=5)
            printf("\t");
    }
    printf("\n");
    for(j=0;j<6;j++)
    {
        if(!a[j][3] && !a[j][0] && !(a[j][2]^a[j][1]) || !a[j][1] && (a[j][3]^a[j][2]) )
            printf("\t|");
        else
            printf("\t ");
        if( (!a[j][3] && ((!a[j][2] && a[j][1]) || (!a[j][1] && a[j][2]) ) ) || (!a[j][3] && a[j][2] && a[j][1] && !a[j][0]) || (a[j][3] && !a[j][2] && !a[j][1]) )
            printf("_");
        else
            printf(" ");
        if( (!a[j][2] && (!a[j][3] || !a[j][1]) ) || (!a[j][3] && !a[j][1]^a[j][0] ) )
            printf("|");
        else
            printf(" ");
        if(j%2!=0 && j!=5)
        {
            printf("\t /");
        }
    }
    printf("\n");
    for(j=0;j<6;j++)
    {
        if( (!a[j][0] && (!a[j][3] && a[j][1] || !a[j][2] && !a[j][1]) ) )
            printf("\t|");
        else
                printf("\t ");
        if( ( (!a[j][2] && !a[j][1] ) && (!a[j][0] || a[j][3]) ) || (!a[j][3] && (a[j][1] && (!a[j][0] || !a[j][2]) || a[j][2] && !a[j][1] && a[j][0]) ) )
            printf("_");
        else
            printf(" ");
        if(!a[j][2] && !a[j][1] || !a[j][3] && (a[j][2] || a[j][1] && a[j][0]))
            printf("|");
        else
            printf(" ");
        if(j%2!=0 && j!=5)
        {
            printf("\t/ ");
        }
    }
}

int get_data()
{
    int variable = 0;
    for(int x=0;x<2;x++)
    {
        variable *= 10;
        int temp = getch() - 48;
        printf("%d",temp);
        variable += temp;
    }
    printf(" ");
    return variable;
}

int main()
{
    system("COLOR FC");
    int a[6][4];
    int n[6];
    printf("\n\n\tENTER THE TIME TO SET (hh mm ss): ");
    int hours = get_data(), minutes = get_data(), seconds = get_data();
    system("CLS");
    int i=hours, j=minutes, k=seconds;
    for(;i<24;i++)
    {
        int temp = i;
        n[1]=temp%10;
        temp=temp/10;
        n[0]=temp%10;
        for(;j<60;j++)
        {
            temp=j;
            n[3]=temp%10;
            temp=temp/10;
            n[2]=temp%10;
            for(;k<60;k++)
            {
                printf("\n\n\n\t\tKARTHIK'S CLOCK SIMULATOR");
                printf("\n\n\n\tTHE TIME IS :\n\n");
                temp=k;
                n[5]=temp%10;
                temp=temp/10;
                n[4]=temp%10;
                binary_converter(n,a);
                display(a);
                _sleep(1000);
                system("CLS");
            }
            k=0;
        }
        j=0;
    }
}
