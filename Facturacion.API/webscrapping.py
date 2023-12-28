import requests
from bs4 import BeautifulSoup


def get_information(url):
    html = requests.get(url)
    instruction = ""

    soup = BeautifulSoup(html.content,'html.parser')

    for el in soup.find_all('li','ui-search-layout__item'):
        instruction += '('
        for name in el.find_all('h2'):
            max_len = min(len(name.text),97)
            sanitized_text = name.text[:max_len]
            if len(name.text) > 97:
                sanitized_text += '...'
            instruction += f'\'{sanitized_text}\','

        instruction += '7,90,'

        for price in el.find_all('span','andes-money-amount__fraction'):
            instruction += f'{price.text}'

        sub_price = el.find_all('span','andes-money-amount__cents andes-money-amount__cents--superscript-24')
        if (len(sub_price) != 0 and len(sub_price) != -1):
            instruction += f'.{sub_price[0].text}'

        instruction += ',1,'

        for img in el.find_all('img','ui-search-result-image__element'):
            instruction += f"'{img['data-src']}'),\n" 

    return instruction


if __name__ == "__main__":
    urls = [
        'https://electronica.mercadolibre.com.ec/audio/cat-ear-headphones',
        'https://electronica.mercadolibre.com.ec/audio/cat-ear-headphones_Desde_51_NoIndex_True',
        'https://listado.mercadolibre.com.ec/celulares#D[A:celulares]',
        'https://celulares.mercadolibre.com.ec/celulares-smartphones/celulares_Desde_51_NoIndex_True',
        'https://celulares.mercadolibre.com.ec/celulares-smartphones/celulares_Desde_101_NoIndex_True',
    ]
    instruction = 'insert into Producto(nombre,idCategoria,stock,precio,esActivo, urlImagen) values\n'

    for url in urls:
        instruction += get_information(url)

    instruction = instruction[:len(instruction) - 2]
    instruction += '\nGO'

    print(instruction)
