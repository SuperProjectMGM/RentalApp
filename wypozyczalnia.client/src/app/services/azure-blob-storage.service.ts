import { Injectable } from '@angular/core';
import { BlobServiceClient, ContainerClient } from '@azure/storage-blob';

@Injectable({
  providedIn: 'root'
})
export class AzureBlobStorageService {

  constructor() { }

  private containerClient(sasUrl: string, containerName: string): ContainerClient{
    return new BlobServiceClient(sasUrl).getContainerClient(containerName);
  }

  public uploadImage(sasUrl: string, containerName: string, content: Blob, name: string, handler: ()=> void)
  {
    const blockBlobClient = this.containerClient(sasUrl, containerName).getBlockBlobClient(name);
    blockBlobClient.uploadData(content, { blobHTTPHeaders: { blobContentType: content.type} })
    .then(() => handler);
  }
}
