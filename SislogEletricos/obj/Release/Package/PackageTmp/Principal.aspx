<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Principal.aspx.vb" Inherits="SislogEletricos.Principal" %>

<%@ Register Src="~/Recebimento.ascx" TagName="Recebimento" TagPrefix="uc" %>
<%@ Register Src="~/Embarque.ascx" TagName="Embarque" TagPrefix="uc1" %>
<%@ Register Src="~/Autorizar.ascx" TagName="Autorizar" TagPrefix="uc2" %>
<%@ Register Src="~/LiberarPortaria.ascx" TagName="LiberarPortaria" TagPrefix="uc3" %>
<%@ Register Src="~/Historico.ascx" TagName="Historico" TagPrefix="uc4" %>
<%@ Register Src="~/Usuarios.ascx" TagName="Usuarios" TagPrefix="uc5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="Icones/icone.png" />
    <link href="Style/Principal.css" rel="stylesheet" type="text/css" />
    <title>SisLog</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upMain" runat="server" >
            <ContentTemplate>
                <asp:PlaceHolder ID="phModais" runat="server"></asp:PlaceHolder>
                <div class="div__header">
                    <h1 class="h1__titulo">SisLog</h1>
                    <nav class="nav__button">
                        <asp:Button ID="btnRecebimento" runat="server" Text="Recebimento" OnClientClick="abrirModal(); return false;" />
                        <uc:Recebimento runat="server" />
                        <asp:Button ID="btnEmbarque" runat="server" Text="Embarque" OnClientClick="abrirModalEmbarque(); return false;" />
                        <uc1:Embarque runat="server" />
                        <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" OnClientClick="abrirModalAutorizacao(); return false;" />
                        <uc2:Autorizar runat="server" />
                        <asp:Button ID="btnLiberar" runat="server" Text="Liberar" OnClientClick="abrirModalLiberar(); return false;" />
                        <uc3:LiberarPortaria runat="server" />
                        <asp:Button ID="btnHistorico" runat="server" Text="Histórico"  OnClientClick="abrirModalHistorico(); return false;"  />
                        <uc4:Historico runat="server" />
                        <asp:Button ID="btnUsuario" Text="Usuário" runat="server" OnClientClick="abrirModalUsuarios(); return false;" />
                        <uc5:Usuarios runat="server" />                     
                    </nav> 
                </div>
                <asp:Image ID="Image1" runat="server" ImageUrl="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAA0wAAAFeCAYAAAC2HJ8OAAAACXBIWXMAABYlAAAWJQFJUiTwAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAACFMSURBVHgB7d2LcRTH2gbg3pVOlV110CWCs47AIgKLCAwRICIwRABEYIjAcgSWI2AdwS9H4HUEQoIqKINW/9d4ZAvQoF1pdq7PU7U1K2l1Hc10v/319IwS8JHj4+P90Wh0PwEA0GlnZ2dPNzc3n6QbGCcAAAAuJTABAACUEJgAAABKCEwAAAAlBCYAAIASAhMAAEAJgQkAAKCEwAQAAFBCYAIAACghMAEAAJQQmAAAAEoITAAAACUEJgAAgBICEwAAQAmBCQAAoITABAAAUEJgAgAAKCEwAQAAlBCYAAAASghMAAAAJQQmAACAEgITAABACYEJAACghMAEAABQQmACAAAoITABAACUEJgAAABKCEwAAAAlBCYAAIASAhMAAEAJgQkAAKCEwAQAAFBCYAIAACghMAEAAJQQmAAAAEoITAAAACUEJgAAgBICEwAAQAmBCQAAoITABAAAUEJgAgAAKCEwAQAAlBCYAAAASghMAAAAJQQmAACAEgITAABACYEJAACghMAEAABQQmACAAAoITABAACUEJgAAABKCEwAAAAlBCYAAIASAhMAAEAJgQkAAKCEwAQAAFBCYAIAACixnlrizZs3k7/++uvuaDT6Nt6cnJ2dTYoPvYz3vczbeN/v8fzw9PT0cHt7e5YAAABWqPHAdHJysheb++/evduNMPTP+y8+v/C+u3m7traWP28WT6fxvp9v3bo1TQAAABVrLDAdHR1NIvj8FE930/VM4rEXVae9psNT/l3G4/GLROvF/8hsY2PjTgIAgAU0Epgi4ORKUQ5LW6kak1SEp+Pj48PoFD+PTvF+qkkEv8fFzwAAAPRI7Ys+FGHpl1RdWPpIhKWd2PwU3+ePYrrfSuXqUmz2EgAA0Du1BqYiXPyY6jFJNQSnoroEAAD0UK2BqbhmaZLqNUn/Bqe7qUKqSwAA0G+1BaaiyrObmjOJxy/xc7wogs6NqS4BAEC/1RaYRqPRD6kddiPo5GrTTzcJTqpLAADQf7UEpggXO2dnZzupXfZuEpxUlwAAoP9qCUzj8Xjpa4ciYM1iM734iPe9TNX7EJxevXr146LBSXUJAACGoa77ME0WeVEOSfP5/EE8Pdze3r40HBU3vN2N1z4ejUaTVJH4eg/j696N4LR/69atp196reoSAAAMQ+33YSpThKXbEZSmZWEpi4/N8k1p82vjc56nak3iaz750lLkqksAADAcrQlMEYDufCkofSq/dnNz8+Hp6em9YvpelSbp36XIP7rGSXUJAACGoxWBKQLPr7lylK4hPu8gh634Gj+n6k3Sv4tDvDg+Pn6YVJcAAGAw2lJhmqYbyGErqk17UW36ZgXVpnO7o9HoxwQAAAxGKwJTVIgOUwWK4PRNPH2wwuAEAAAMRGuuYapSsSjEqqbpAQAAA9GWwFT5/ZVqmqYHAAD0WCsC01dffXWUVsQ0PQAA4Lp6OSXvMqbpAQAAyxpMYMpM0wMAAJaxngaouOfTNycnJ3sRnB6PRqNJqlF8z8Oodj1KfGRtbe1FAgCAFmlFYHr79m2+xujPVLM8Te/o6Gg6Ho+fRGi6n2oSYenedW/U22cRYBMAALTJoKbkXebCNL3bNU3T2xeWAACgG1oRmNbW1rZSwyLEHObV9CI0PVpVcIqv+zKC2dMEAAB0QlsqTI0HpnMRmp7N5/MHaTWeqy4BAEB3DH5K3mWi4lX59Uy5ahVh7EkCAAA6oxWBKcLEJLXE0dHRJDZ7qWKj0chUPAAA6BgVpk9EdelxqlheRjyvyJcAAIBOaUVgiurL/1ILrKq6lJcRTwAAQOe0ZUredmqBVVSXkmXEAQCgs9pSYdpMDVtVdcky4gAA0F1tuYZpkhq2omuXnqouAQBAd7kPU1pNdcky4gAA0H3rqR22Tk5O9kaj0Sy/EduXUfF5+fXXX89SDVZRXbKMOAAAdF+lgSmHntj8lK7np6jKfHiSt/P5PB0fHz9ddZVmhdWl/QQAAHTa4O/DtIrqUoS9OwkAAOi8QQemFa2MZxlxAADoiUEHplVUlywjDgAA/THYwLSia5csIw4AAD0y2MBUdXXJMuIAANA/gwxMq6guWUYcAAD6Z5CBaRXVpY2Njf0EAAD0yuAC0yqqS5YRBwCAfhpcYFrByniWEQcAgJ4aVGBaRXXJMuIAANBfgwpMK7h2yTLiAADQY4MJTFVXlywjDgAA/TeYwFR1dcky4gAA0H+DCEyrqC5ZRhwAAPpvEIGp6urSfD6/lwAAgN7rfWBawcp4eRnxwwQAAPRe7wNT1dUly4gDAMBw9DowreDaJcuIAwDAgPQ6MFVZXcoLPczn82cJAAAYjN4GpqqrS3kZ8aguvUwAAMBg9DYwVV1dsow4AAAMTy8DU9XVJcuIAwDAMPUyMFW8Mp5lxAEAYKB6F5iqri5ZRhwAAIard4Gp4muXLCMOAAAD1qvAVGV1yTLiAABArwJTldUly4gDAAC9CUxVV5csIw4AAPQmMFVZXZrP5w8SAAAweL0ITBWvjJeXEZ8mAABg8HoRmKqsLllGHAAAONf5wFTxtUuWEQcAAP7R+cBUVXWpWEZ8PwEAABQ6HZiqrC4Vy4jPEgAAQKHTganK6pJlxAEAgE91NjBVWV2yjDgAAHCZzgamClfGs4w4AABwqU4GpiqrS5YRBwAAynQyMFV47ZKFHgAAgFKdC0xVVZcsIw4AAFylc4FpfX39fqqAZcQBAICrdCow5epSVIb20g1ZRhwAAFhEpwJTUV2apBuyjDgAALCIzgSmqqpLyTLiAADAgjoTmKqqLllGHAAAWNR6qtbLeExTRUaj0SxvK6wuzSz0AAAALKrSwLSxsXEQm4NUsVxdisA0SQAAADVq/ZS8CqtLAAAAS2l9YKrq2iUAAIBltTowqS4BAABNanVgUl0CAACa1NrApLoEAAA0rbWBSXUJAABoWisDk+oSAADQBq0MTKpLAABAG7QuMKkuAQAAbdG6wKS6BAAAtEWrApPqEgAA0CbrqUXG4/HdtNrq0tbJycmLBAAAsIBWBabRaPRDWq2teOwmAACABbRmSt6rV692k2uXAACAFmlNYDo7O7ufAAAAWqRNiz7sJgAAgBa58TVMb968mbx//37y2RdeX5+9ffv25fb29survsbJycmqF3sAAABY2tKBKS/9ne+VNJ/P745Go513795d+rr8/rW1tRyG8nS7w3jty3gcxuf9OR6PDyNkHV4IU6bjAQAArbNUYHr9+vUPEXieRQDKK9ot/Hk5WOVtfN5u/rz8+UWYmsW7D+NxNwEAALTMwoHp1atXjyMsPUnVmiRT8QAAgJZaaNGHHJaiKvQkAQAADMiVgUlYAgAAhuqLgSmvXicsAQAAQ1UamPJqeLH5KQEAAAzUpYEpwtLW2trai3i6lQAAAAbq0sC0vr7+OFm9DgAAGLjPAtPJycne2dnZwwQAADBwH92Hqbhu6XGi1+bz+W8RivdTS6ytrX2f3LwYAIAW+igwRcfVVLwBGI/Hf2xsbOynljg+Pp6MRiOBCQCA1vlnSl6eihebvQQAAMAHHwKTqXgAAACf+xCYTMUDAAD43NhUPAAAgMvlCpOpeAAAAJfIgWmSAAAA+Mw4AQAAcKn1xBDdPT4+3k0tMRqNthIAALSQwDRMW0IKAABczZQ8AACAEgITAABACYEJAACghMAEAABQQmACAAAoITABAACUEJgAAABKCEwAAAAlBCYAAIASAhMAAEAJgQkAAKCEwAQAAFBCYAIAACghMAEAAJQQmAAAAEoITAAAACUEJgAAgBICEwAAQAmBCQAAoITABAAAUEJgAgAAKCEwAQAAlBCYAAAASghMAAAAJQQmAACAEgITAABACYEJAACghMAEAABQQmACAAAoITABAACUEJgAAABKCEwAAAAlBCYAAIASAhMAAEAJgQkAAKCEwAQAAFBCYAIAACghMAEAAJQQmAAAAEoITAAAACUEJgAAgBICEwAAQAmBCQAAoITABAAAUEJgAgAAKCEwAQAAlBCYAAAASghMAAAAJQQmAACAEusJOurs7Gx2/nw0Gs0ufGh24TXHsXl5/vZ8Pj9MAACwIIGJWlwINy8j3OQAc/7IH/vz/GMRaM7DzT8fTxcC0Pb29iwBAEBNBCYWch54ikrOhzBzoXpzHnQ+e0TAeZkAAKCjBKaBirDzIdAUAWh2Hn4i+OS388fOt0IPAACDJTD1WK4KRSCa5jBUXLuTg8+HrRAEAABXE5h6IleMIhwdnJ6e/hZvHkYgsrgBAADckMDUccW1Rc+jgrSvagQAANUSmDqquAbp6ebm5rMEAACshMDUTQdRUXqgogQAAKslMHVMVJYeqSoBAEA9BKaOKBZ1eBBh6SABAAC1EJg6Yj6f37HyHQAA1Etg6oYHwhIA8CXnN6XPz0ej0STRe/Z5PQSmlosDIa+Et58AgMEqOsb5hvSH8fz3+Xw+y2+nv29GPyv7vKOjo63YTOKxNR6Pd/Lz+BrfxtfYie1WotXy7WNiP01j+2exz/MA+oeQVLb4V7HPP9rvxT7P+34nsTSBqd2mEZaeJABgiHJH+dfoKOdO8uF1VsctPud8lsr04seiY72ztra2E99jN978ToWiebEv8r76LfZ5vmb9Jvv8Q8Au3vXP9e9FmNqNEHU32ecLE5haKo8k5aXDEwAwJNPT09OfY3uw6tuHFNP982M/vx2d6dyR3ks60nU7D8b7Nezz/PUPikfe55MiPP1gn5cTmNrr6ZdK7ABAPxTT7Z5Hh/lZk/dYjO89TUUVKjrSd6P69EM83U1U7sI+nxZ/90YUfc18u5pnueIY4elhBKf7iY+ME62T56u61xIA9F6uJt2JNn87T8Fv0w3p42c52NjYuBM/3zfRL/k5UYkclPL16RGUvin2+TS1RK44xs+0V+zzp7k/mvhAYGqhOIgeJQCgl/J1Kjko5UDSpg7zZXIF4kInWnC6pkuCUmvC8aeKff4k39Im/8ypWIVvyASm9slzlt2cFgB6pug0P4rO6O22B6VPfRKc3OpkCV0JSp86D06xz28PPSwLTC0T/5SqSwDQM9HhfF50mjs95b7oRN+Opw9M2bpSnnJ5u2tB6VOfhOVZGiCBqV32LfQAAP2RO5jFdUoPu9xp/tTGxsZ+nrKVLixZzb9yJbGYctmbalwRnL4ppukNisDUInFCHdw/IAD02DRCReem3y0qd6AjFNwbYge6TBGQb/d58a5imt6dIVWbBKb2UF0CgJ7IIaKoMPT+gvkL17nM0rAdFAG599d45UGAYlGIQVzPJjC1hOoSAHRfcX+dBzlEpAHJIaHoQM/SABUB+d4QAvK58+vZ8vV5qefcuLYdpl2oLh0dHW395z//mcQJcScOjnxn6M3YbsWHzh8X5RNGXg3oz9FoNMuP9+/fHw7pRALAsOSwlEPDKisM122L8zZee7jKtjj3ZeLnux3f50W0+ztpIHJYWnVAfvPmzeTdu3f5b7qV93v8ff9XfGhyyctzvyv/Lx7H9jA/X+V+z9fnHR8fv4zv8zj1lMDUDq1cqvH169c7UfnajQPguzg4d2KbT9AfPhbP8wniyq+RX5fl166traWTk5N8sB7G27/FCXV669ataWqZ+Jmnsbn6lwOAC6KNfFr1AOiFtvjbaDt3q2yLc2c6tr9W2RYXnfLb0YF+cqFT32e/RWDYTxXKoXh9fT2Ho+9z/yu2OxGW/gnD5/vzS/K+/nS/xz7J+3yW+2Dx9vS///1vZcE+B8YiNH2bWqb4P7/Z14iDRsewYXEi3G5D5eX8AI0D6X68eTd9PlK1KnmFnV/j79CJShsArNKrV69209+d5b1UX1s8jcfP2uJm5D5YDCTvRef++3jzQyUprd4sHtP4nj+3cQC7TQSmhsXJ8NdI5XdTgxo6MZfZd+ACMDS5khTtcG6LH6bm2+I8kPnzxsaGJcNXLPfBYp/nqWy7qVmz9Pd9o54KzJ8TmJr3IN/LIDWgRQfpZWbxeNrU3wYA6tCFtljVqVrFjJ4fWhKOL5OrTk8NXv9LYGpYXoaz7uUnW35y/tQsCU4A9EzX2uLoQO9HB9qKvjfQgaD0kbxk+Hg8fiQ4CUxNexlBYDvVpGMn50/NkuAEQMdFp3lnbW3tx6QtHpTXr1//MJ/Pn6QOBKVLHMQA/6MhVxkFpmZN803t0ooVIxqPixGNrsvzax+YGgBAl/SsLZ5FW3xHW3y1jgfkj0SV8clQq4xuXNugOGn+nlYsV5XiQP2/npygs/z7/BG/V2/X+gegX3rYFk+0xVfLf5+831MPwlIW/79PotDyRw6BaWAEpgZVsS78l8SB+mP8c79Il9/UrNOKg/ZFHLSTBAAtNYC2+A9t8cfy3yP2ew7IT1L/5LD8f0MLywJTg6KcPUsrcOFA7ctIVpk8YvdiiCMdALRbbovzwN4A2uJJ0RY3eouUtoh9freoJva6bzK0gWuBqUFfffXVH6lixVzZF30/UC/4MNJxfHzc9wYJgI7IncjcFqeeTMVaQP59fxn6FL3i9/8ldXNhh+s4H7iepJ4TmBr09u3b41Sh169f3y9O0JM0MKPR6EdzqQFo2vn1SmmAbXGuOgy1LS6mXj5Jw/Nh4Lrvs30EpgZtb2+/TBXJYWk+n++n4YxqfGbIJ2oAmpfb4uJ6pUG3xScnJz+lAcm/7wCmXn7JVh6wz///qacEpubMUkUuhKXBE5oAaIK2+CN7QwlNxe+5l9jK//99DU0CU8c5QX9OaAKgTnkanrb4M3t5mlrqseL320v8Ix8HfZyeJzB1WP6HdIK+nNAEQB3yBe/R5vyS+EyeptbXtjj/XgOfhleqjysYC0wNiYPsRtcvFSvwOEF/QQ5NfZ5PC0CzLqyGN9hrlq6S2+K+rWQbfYsfBrrAw6LyNU2/9Gn1PIGpIaPR6NqBKf4Bt4a6Gt6yogL3zA31AFgFbfFi8kq2fak4FLN7niWuMhmPx70Z2F9PdE6coPOc2UliEVtFafj2sqsSFhdyThIAvRYd+sNbt249WuZzimWkJ4mFFBWHhdviaIP3YlPLLJHYj79tbm4+uep1ZvcsJ46rnXycLHtsZbH/871KJ6kZ+xsbGw8uvkNg6pjiBLKXWMZkfX09z6Fe9oDdTQITQO9Fh3mp1+e22PUrS8thIw9E3lvic3ZTDaJjP1vkdfHz577EJLGwfJxE0Pw1gvI0dZgpeR1STC2zkME1FAfsbgKAG9AW38jdrl7PZMD6+nJQzpeTpA4TmBoSHfjf05KMbNxMHw5YAJqlLb6ZqOY8XqQtXrTqUwch+cbOZ/p0lsDUkDxfepnXG9moROcPWACaE23x3aQtvqmt4lrsL3r//v0stYSQfHNdn+kjMDXk9PR0mpajo1+B4oCdJABYXq9vxFqjvQU6zze6/UpVXr9+nVf320vcWBE8O0lgakB02mfb29uzRV9fVJcmiUoUF50CwMK0xdW6qvO87Mq2qxID3PoM1dktjqO224uf8+ziwyp5DRiNRk/TclSXqrWbR7a6vmILALXSFldrkbZ4lqoPqfuXvO+3y15YdO57cf+oFsnH0X7qGIGpZrm6NJ/Pp4u+3ojWahQjW9MEAFfQFq/GVW1x9Jmex+bSBSJi8Dnfo2mSlvTp/XW+JL7HD8suOc+VJvl4iv2w/6UXRWXvwXg83o2nW7Efvs33PIvtJDVklMtMidrEDn+6yM3RzjV8465ei4PxzpdGtvztAQZjGh24O2Uf1B6szlVtcZnYJy/SNe7TFPt5tMjrXr16tRt9theJVfji8XaZYtCisemRrmGqUa4uLRmW9pIT9MrEyMXdBMBQ3IvO+XbuMOdHPP8md9ajbX4Uj1/LPqlYGW+SWIm2LgQQ/xP3E6uy27UV80zJq9E1rl1ysK5QLufHAfukLReWArAa0fk9jAHLg4vvKxZfyo/pFZ+uLV6t3HnealNbXKymu5dYma5dGqHCVJM8D/eq+ZoXFQfrbmKVtqLKtJcA6LUYILtWZ7xoi81GWLG2tcXRmd9NrNrOIjcwbguBqQbFyNbDZT7HdLF6RCP6fQKg72bpGnSc69G2tjgv9pBYtU4NWgtMK1asincvLSn+iUwBqMdul0Y4AKiVtrgeu2/evPlfaoFcVYy+m6XEa9ClQWuBaYWKsHRnmZvUZg7WesUIomoeAB8xNb5ef/3119KDy6tghk+tOjNobdGHJeTVdNJyDq9zEaMpALX7LnXwJmoArE60xQYua1RUG56lhpmqX6+iz3uQWk5gWsJ17hNwTQ7WeuXRpIVvZAfAIGiL69WWgLqbqM3Z2dlu6khgmn7pBfGL5DvsGmWpl793vbby3Omvv/76zwRA70RfZjstKfd94vMStdk6OjraicHpw9SQ4ma1iVp9l1oi9n2eFXbpzLD1q+60607H9SrmTE8StXr37l0+DvYTAL0T4Wdzmdfn6ypcS1y/8Xi8G5vGAtN8Pt+J/5VEffLARFvuwxU/y0HkoktnHFn0oWXMmW6GhhGgv/JsmWVev76+rk1oQHRYv00Niu/fmmrHwExSywlMLaPj3phJAqCXoiO8VGDKlYZE7VrQB3KbkQZ0oVggMLXPJFG7pke1AFipZTvCk0Ttoi2epGYJyg3oQrFAYGqZOFm04sZtAzRJAPTVUoFJW9yYrabuy1N8XxWmBix7jWETrlxW/P3797PxePw0UYs8bcAKLc34dKW82A/Pk5MnQC8seWG5c39Dvvrqq9x5bmIBgEmiKZPUclcGpji5zGLzJFGL6KRPEq2wubnZ+A30AGjEJNGIt2/ffhOb2m/zsb6+bsC6OZPUcm5c2z5GtRrS1EkagNbRFjdkbW1tob99hJvcXs9SRZZdSZF6nZ6eHq56xtt8Pi9d0l5gAgD4mM5zcxb6229ubu6latnnzbnyb1/c0Lixe3RZ9AEAAGhK68OqwAQAAFBCYAIAAJrSxKqISxGYAAA+Nks04vT0dJaa0fpOe48JTCxtlmiKkyUADNBoNNIHaMjZ2ZnABB0ySwAMns5zoxr5279//36WaEQXjjeBqX0aWzJx6Ja4AzwAPTafz39PNGWWmqEP0JCoMB2nlhOYWqYL/zR9FH93QRWAczrPzXjZ1OBl8X3t92a0vg8mMLWPjnsDTL8A4Fy0CdriZjT6d4/9PkvUrgvHm8DUMg7WZkSFyfQLAD44PT0VmBrQdFtsKmYz4nhzDRPLiX+aaaJ2EVSnCQD+Nks0YZaaJSg3YHt7e5paTmBqmWIO7SxRK6OJAJzLbbFpefWLCs80Najp7z9Q09QBAlMLRUn6t0Rt4u89i8ZxlgCgEJ1nbXG98oIPjYbU4vu7prlGXbkkQmBqJ6NaNTIdD4BPaRvq1aLBYn2wGnXlOBOYWihGtfYTdTKKCMBHXFNcr+g4H6QWiOD2a6I2XTnOBKYWKq5jmiZqEQdrK07SALSHtrhebek4x6C1PkF9pk3dd2tZAlNLuY6pNp05WAGol2pDbaZtuZa4+DmmiTr8nDpCYGqpGOF4lqhDZw5WAOpVTJE3qLZ6rWqLBeV6dGmGj8DUUqYCrF5eHW9jY2M/AcAlirbYIgAr1rbrWATlWux3aYaPwNRicQJ5mlgZKyABcBVt8crtt+3WHgatVy+Oq07N8BklWu34+PiP6NhPEpWLg/Ub918C4ConJycvYrObqFxb2+Kjo6PdtbW1F4nKnZ2dHW5ubt5OHaLC1H6usVmN1o1oAdBa2uLVaM1iD5+Kn2uaVJlWIgoBz1PHqDC1XIxwbMUIxx/xdCtRGdUlAJZhxkf1oi2+UwSTVlJlql6+fjyqS9+kjlFhark8jzb+ucyfrpbqEgBLmc/njxJV2m9zWMpUmaoXgw6d7NOqMHWEka3qqC4BcB2uZapOV9piVabqdPHapXMqTB0RI1sPEjeWq3XCEgDXYcW8ynRmpkeuMkXfoXPX3LRR9GXvpY4SmDqiKAt35gZfbVTMm32SAOAadJ5vLrfFXQue0dF/ktyX6UbycdPlAWuBqUPiBJOrTA7Ya1KlA+Cmcuc5d/oT15KvYelaxzlfT170wbiGfLwUobOzBKYOccBeXzEVb5oA4AZyW2wA7npylWFjY2M/dVDs94P4+S0vfw15Kl5xM+DOEpg6pjhgTQdYgql4AFTJ1Lzl9aHKED//Q9XF5RQD1oep4wSmDiqmA3T+n68OxQn6TgKACsVA3MNkyemFRFucq3J3ul5lKKqLeeECl0cs5qAvA9YCUwedH7BGOa6Wp01YFQ+AVTg9PdUWL2A0GvWmLc7Vktjv7sl1hWJxj978nQSmjsonHqMcXxYH6yPXLQGwKsUAZp7FoC0ukadkbWxs9GqV39jv+/n3SlzqfHZPnwasBaYOK0Y5Orum/SrlE1mUgZ8lAFih3CmMtlhoukTRFj9JPZR/L6Hpc8X0y3t9m90jMHVcrqBYOe9jfT5BA9A+xQCm0HTBENpioeljF65V69119qNELxwdHe2tra39lAZOWAKgKdEW70Rb/CKebqUBG1pbfHx8/GQ0Gj1OA9bnsJQJTD0y9BO1sARA03JbPB6Pf4kO9CQN0FDb4iGHpuKapXt9DUuZwNQzcaKexIn6xZBO1HlUI37fR129GR4A/TLEtjjLiy0N+frhCE0PY5//mAakjws8XMY1TD1TrJ6X51FP0wCcH6jCEgBtUbTFt6ON+jkNQLGE9O2hL7aUf//8dxjKUvP5/zv/nw/h9i0qTD02gPLwQV7wous3wgOgvwbQFufFp+5pi/+VK4zFdeW7qYfyzJ7YDGo1YoGp5/o4LWCIByoA3aUtHqY+TtGL/X4YVaUHfb5e6TIC00D0aIQrV5UeDaH8C0C/9KgtnhYzPGaJLyqqTTk03U0dVgTk50NdXEtgGpBihCufrO+n7skn56f5vlMJADqqy21xcd3wA23x8k5OTvbi7/e4o1XG/aIPNksDJTANUJysd2O0I49w7aaWyyfnOLk8tagDAH3SpeCkLa5Ox4KTweqCwDRgOTjFyXqvpSfrfJA+j4P0IAFAT7U8OE3j8bOgVL2WB6eDog82TXwgMHF+st6Lp/ebPHCL+bF5icoDBykAQ1Jc67LbdCdaW1yv88HrePp97Pet1JBiKfS8359Z8fBzAhMfKQ7cfGHid3Hg7qQVK246m0cy8r0qDh2kAAxdtMU7RSdaWzwQsc9zWDrvg9USnvKKd7H5NULSVDj+MoGJUhdGu3bjzW+rOGkX86Cnsf29OEAHtSwlACwjt8WxyQFqN9rPb1MF1x9ri9uvGMDeif30XeynyU37YEXlcBaP32Kf5/19IBgvTmBiKXnUKzZb+SDO2/yIg3jzstfGwflnbF7GgTlLfx+kMwcnANzMeVscjzylfpIWa4tz+5s7ytriDioqUOd9sEn6e5//77LXxj4/Tv/2vz7sd0vA38z/AwZ0muviNQI2AAAAAElFTkSuQmCC" CssClass="icone" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script>
        function abrirModal() {
            document.getElementById("meuModal").style.display = "block";
        }
        function fehcarModal() {
            document; getElementById("meuModal").style.display = "none";
        }
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
        });

        function abrirModalEmbarque() {
            document.getElementById("modalEmbarque").style.display = "block";
        }
        function fehcarModal() {
            document; getElementById("modalEmbarque").style.display = "none";
        }
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
        });

        function abrirModalAutorizacao() {
            document.getElementById("modalAutorizacao").style.display = "block";
        }
        function fehcarModal() {
            document; getElementById("modalAutorizacao").style.display = "none";
        }
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
        });

        function abrirModalLiberar() {
            document.getElementById("modalLiberar").style.display = "block";
        }
        function fehcarModalLiberar() {
            document; getElementById("modalLiberar").style.display = "none";
        }
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
        });

        function abrirModalHistorico() {
            document.getElementById("modalHistorico").style.display = "block";
        }
        function fehcarModalHistorico() {
            document; getElementById("modalHistorico").style.display = "none";
        }
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
        });

        function abrirModalUsuarios() {
            document.getElementById("modalUsuarios").style.display = "block";
        }
        function fehcarModalUsuarios() {
            document; getElementById("modalUsuarios").style.display = "none";
        }
        document.addEventListener("DOMContentLoaded", function () {
            document.getElementsByClassName("close")[0].addEventListener("click", fecharModal);
        });

         // A cada 9 minutos (540.000ms), envia uma requisição para manter a sessão ativa
        //setInterval(function () {
        //    fetch('KeepAlive.aspx');
        //}, 540000);
        
    </script>
</body>
</html>
