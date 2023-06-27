# CommandCentral API
API for the CommandCentral App

To run the API and database as a backend, start downloading the docker images.
Next use docker compose to build and run the application.

- Run the following:
  - `docker compose build`
- Then to run the images:
  - `docker compose up`
- verify everything is running either in the browser using swagger or in postman, with the following url:
  - `http://localhost:8080/swagger/index.html`


# Docker images
## API image
[API image](https://hub.docker.com/repository/docker/kristians93/command_central_api/general)
## Postgres image
[Postgres image](https://hub.docker.com/repository/docker/kristians93/command_central_postgres/general)


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% By J. Leon
\documentclass[border=20pt]{standalone}
\renewcommand\familydefault{\sfdefault} % Default family: serif 
\usepackage[usenames,dvipsnames]{xcolor}
\usepackage{tikz}
\usepackage{soul}
\usetikzlibrary{calc} 
\usetikzlibrary{arrows, decorations.markings,positioning,backgrounds,shapes}
\definecolor{EMP}{HTML}{77DD77} % Green1
\definecolor{NOR}{HTML}{06500C} % Green2
\usepackage{ulem}
\renewcommand{\ULdepth}{3pt}

\newbox\ubox
\begin{document}
\begin{tikzpicture}[
    EMP/.style={% Style for empatized boxes
        rectangle, line width =1pt,
        anchor=west,
        underline, % new property
        align=center,
        text=Black,
        minimum height=.8cm,
        text height=1.5ex,
            text depth=.25ex,
        fill=EMP,
        draw=black,
        },
    NOR/.style={% Style for normal boxes.
        rectangle, 
        line width =1pt,
        anchor=west,
        align=left,
        minimum height=.6cm,
        text height=1.5ex,
            text depth=.25ex,
            text=white,
        fill=NOR,
        draw=black,
        inner ysep=5pt
        },
    underline/.append style={% define new style property
        execute at begin node={%
            \setbox\ubox=\hbox\bgroup
            },
            execute at end node={%
                \egroup\uline{\box\ubox}%
                }
             },
    ] % Uff that is all the configuration for tickzpicture xD

% Define an brute force objet "Frame"
% Variables 1:Position, 2: Identifier, 3: Title of frame 4: Subframe/Boxtype
 \def\Frame(#1)#2[#3]#4{%
  \begin{scope}[shift={(#1)}] 
      \node[font=\bf, anchor=west] (Title) at (-0.2,0.7) {#3}; 
       \edef\k{0}% Variable for box positión
       \edef\x{0}% Variable for named coordinate centering - below box
       \foreach \id/\style in {#4} {%enter sub frame data Name/Boxtype ,Name2/Boxtype | An space before Boxtype is needed 
            \node[\style] (h) at (\k pt,0) {\id}; %  % Draw a node depending on the variables.
            \pgfmathparse{\k+0.5*width{"\id"}+3.4pt} % Uses the textwidth to calculate named coordinate  
            \xdef\x{\pgfmathresult} % The resul is saved in the variable \x
            \draw (\x pt,-0.4) coordinate (\id#2); %Create a named coordinate concatenated: "sub frame data Name"+"identifier"
            \pgfmathparse{\k+width{"\id"}+6.8pt}% Calculate positión for each subframe box.       
        \xdef\k{\pgfmathresult}% Save the value to be added to the next iteration value.
       }    
  \end{scope}
}% disadvantages: Is not posible to use Frame data Name like: Name_another_desc instead I use Name-another-desc

% Start drawing
% \node[EMP node] (dm) at (0,0) {{Sometext/EMP,another/EMP}};
 \Frame(0,0){1}[EMPLOYEE]{%first frame identified as 1 named EMPLOYEE
    Fname/NOR,% see that it is necessary to add a space
    Minit/NOR,
    Lname/NOR,
    Ssn/EMP,
    Bdate/NOR,
    Address/NOR,
    Sex/NOR,
    Salary/NOR,
    Super-Ssn/NOR,
    Dno/NOR}; 

 \Frame(0,-2.5){2}[DEPARTAMENT]{
    Dname/NOR,
    Dnumber/EMP,
    Mgr-ssn/NOR,
    Mgr-Start-date/NOR}; 

 \Frame(0,-5){3}[DEPT\_LOCATIONS]{
    Dnumber/EMP,
    Dlocations/EMP};

  \Frame(0,-7.5){4}[PROJECT]{
    Pname/NOR,
    Pnumber/EMP,
    Plocation/NOR,
    Dnum/NOR}; 

  \Frame(0,-10){5}[WORKS ON]{
    Esgn/EMP,
    Pno/EMP,
    Hours/NOR};     

 \Frame(0,-12.5){6}[DEPENDENT]{
    Essn/EMP,
    Dependent-Name/NOR,
    Sex/NOR,
    Bdate/NOR,
    Relationship/NOR}; 

% Start drawing arrows:
% In this part I use the named coordinates to draw the arrows.
    \draw[thick,<-,thick,>=latex] % From Essn6 to Ssn1  
        (Ssn1)++(0.1,0) -- ++(0,-.55) -- ++(4.5,0) coordinate (inter) %inter is the name of coordinate register
        -- (Essn6 -| inter) -- ++(0,-0.4) coordinate (inter)  % to calculate intersections.
        -- (Essn6 |- inter) --++(0,0.4); %
     %Essn -- Ssn id 5
     \draw[thick,<-,thick,>=latex]
        (Ssn1)++(-0.1,0) -- ++(0,-.7) -- ++(4.55,0) coordinate (inter) %some shift using (Ssn1)++(shiftx,shifty)
        -- (Esgn5 -| inter) -- ++(0,-0.4) coordinate (inter) 
        -- (Esgn5 |- inter) --++(0,0.4); %
     \draw[thick,<-,thick,>=latex]
        (Pnumber4) -- ++(0,-.5) -- ++(1,0) coordinate (inter) 
        -- (Pno5 -| inter) -- ++(0,-0.2) coordinate (inter) 
        -- (Pno5 |- inter) --++(0,0.2); %

     \draw[thick,<-,thick,>=latex]
        (Dnumber2) -- ++(0,-.75) -- ++(4,0) coordinate (inter) 
        -- (Dnum4 -| inter) -- ++(0,-0.2) coordinate (inter) 
        -- (Dnum4|- inter) --++(0,0.2); %

     \draw[thick,<-,thick,>=latex]
        (Dnumber2)++(-.2,0) -- ++(0,-.9) -- ++(1.75,0) coordinate (inter) 
        -- (Dnumber3 -| inter) -- ++(0,-0.2) coordinate (inter) 
        -- (Dnumber3 |- inter) --++(0,0.2); %

     \draw[thick,<-,thick,>=latex]
        (Ssn1)++(-0.3,0) -- ++(0,-0.85) -- ++(3.5,0) coordinate (inter) 
        -- (Mgr-ssn2 -| inter) -- ++(0,-0.2) coordinate (inter) 
        -- (Mgr-ssn2 |- inter) --++(0,0.3); %

     \draw[thick,<-,thick,>=latex]
        (Dnumber2)++(0.2,0) -- ++(0,-.6) coordinate (inter) -- (Dno1 |- inter) -- (Dno1); %

     \draw[thick,<-,thick,>=latex]
        (Ssn1)++(0.3,0) -- ++(0,-.4) coordinate (inter) -- (Super-Ssn1 |- inter) -- (Super-Ssn1); %

\end{tikzpicture}
\end{document}
