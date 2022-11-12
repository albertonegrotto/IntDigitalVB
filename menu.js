const btnDepartamentos = document.getElementById("btn-departamentos");
const btnCerrarMenu = document.getElementById("btn-menu-cerrar");
const grid= document.getElementById("grid");
const nwrecord = document.getElementById("nuevoRegistro");
const contenedorEnlacesNav = document.querySelector('#menu .contenedor-enlaces-nav');
const contenedorSubCategorias = document.querySelector('#grid .contenedor-subcategorias');
const esDispositivoMovil = ()=> window.innerWidth<= 800;
console.log('ContenedorEnlacesNav', contenedorEnlacesNav);

btnDepartamentos.addEventListener('mouseover', () =>{
    if(!esDispositivoMovil()){
     grid.classList.add('activo');       
    }

});

grid.addEventListener('mouseleave', ()=>{
    if(!esDispositivoMovil()){
        grid.classList.remove('activo');
    }
});

document.querySelectorAll('#menu .categorias a').forEach((elemento)=>{
    elemento.addEventListener('mouseenter', (e)=>{
        if(!esDispositivoMovil()){
            document.querySelectorAll('#menu .subcategoria').forEach((categoria)=>{
                categoria.classList.remove('activo');
                if(categoria.dataset.categoria == e.target.dataset.categoria){
                    categoria.classList.add('activo');
                }
            })            
        }

    })
});

document.querySelector('#btn-menu-barras').addEventListener('click', (e)=>{
    e.preventDefault();

    if(contenedorEnlacesNav.classList.contains('activo')){

        contenedorEnlacesNav.classList.remove('activo');
        document.querySelector('body').style.overflow = 'visible';
    }
    else{

        contenedorEnlacesNav.classList.add('activo');
        document.querySelector('body').style.overflow = 'hidden';
    }
    
});

btnDepartamentos.addEventListener('click', (e) =>{
    e.preventDefault();
    grid.classList.add('activo');
    btnCerrarMenu.classList.add('activo');
});

document.querySelector('#grid .categorias .btn-regresar').addEventListener('click', (e)=>{
    e.preventDefault();
    grid.classList.remove('activo');
    btnCerrarMenu.classList.remove('activo');
});

document.querySelectorAll('#menu .categorias a').forEach((elemento)=>{
    elemento.addEventListener('click', (e)=>{
        if(esDispositivoMovil()){
            contenedorSubCategorias.classList.add('activo');
            document.querySelectorAll('#menu .subcategoria').forEach((categoria)=>{
                categoria.classList.remove('activo');
                if(categoria.dataset.categoria == e.target.dataset.categoria){
                    categoria.classList.add('activo');
                }
            })
        }
    })
});

document.querySelectorAll('#grid .contenedor-subcategorias .btn-regresar').forEach((boton)=>{
    boton.addEventListener('click', (e)=>{
        e.preventDefault();
        contenedorSubCategorias.classList.remove('activo');

    })
})

nwrecord.addEventListener('click', (e)=>{
    
    if(document.querySelector('.newRecord').classList.contains('activo')){

        document.querySelector('.newRecord').classList.remove('activo');

    }
    else{

        document.querySelector('.newRecord').classList.add('activo');

    }
})

