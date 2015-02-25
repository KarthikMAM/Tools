#include<stdio.h>
#include<conio.h>
#include<windows.h>

using namespace std;

int main()
{
    system("COLOR FC");
    start:
    printf("\n\n_______________________________________________________________________________");
    printf("\n\n\tENTER A NUMBER : ");
    int n[9],a[9][4],temp,j;
    for(j=0;j<9;j++)
    {
        n[j]=(int)getch()-48;
        if(n[j]<=9  && n[j]>=0)
        {
            printf("%2d",n[j]);
            temp=n[j];
        }
        else
        {
            printf(" INVALID ENTRY TRY AGAIN");
            goto start;
        }
        for(int i=0;i<4;i++)
        {
            a[j][i]=temp%2;
            temp=temp/2;
        }
    }
    printf("\n\n_______________________________________________________________________________");
    printf("\n\n\n\tTHE EQUIVALENT LED DISPLAY OF");
    for(j=0;j<9;j++)
        printf("%2d",n[j]);
    printf(" IS : \n\n\n");
    printf("\n");
    for(j=0;j<9;j++)
    {
        if( (!a[j][3] && a[j][1]) || (!a[j][3] && a[j][2] && !a[j][1] && a[j][0]) || ( (!a[j][2] && !a[j][1]) && (a[j][3] || !a[j][0]) ) )
            printf("\t _");
        else
            printf("\t  ");
    }
    printf("\n");
    for(j=0;j<9;j++)
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
    }
    printf("\n");
    for(j=0;j<9;j++)
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
    }
    printf("\n");
    x:
    printf("\n\n_______________________________________________________________________________");
    printf("\n\n\n\tDO YOU WANT ANOTHER TRY (Y/N) : ");
    char op=getch();
    if(op=='Y' || op=='N')
     {
         printf("%c",op);
         if(op=='Y')
            goto start;
     }
    else
    {
        printf("INVALID OPTION TRY AGAIN");
        goto x;
    }
    printf("\n\n_______________________________________________________________________________");
    return 1;
}
